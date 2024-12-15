using MediaMarket.Application.Configs;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Infrastructure.Prompts;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace MediaMarket.Infrastructure.GenerativeAI
{
    public class GeminiAIService : IGenerativeAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _geminiUrl;
        private readonly ILogger<GeminiAIService> _logger;
        private readonly Dictionary<string, PromptStruct> _prompts;

        public GeminiAIService(IOptionsMonitor<AIGenerativeConfig> config, ILogger<GeminiAIService> logger)
        {
            _prompts = new Dictionary<string, PromptStruct>();
            _apiKey = config.CurrentValue.ApiKey;
            _geminiUrl = "https://generativelanguage.googleapis.com/v1beta/models/" + config.CurrentValue.Model;

            _httpClient = new HttpClient();
            _httpClient.Timeout = Timeout.InfiniteTimeSpan;

            _logger = logger;

            LoadPrompt();
        }

        public void LoadPrompt()
        {
            var prompts = typeof(IPromptItem).Assembly
                .ExportedTypes.Where(x =>
                    typeof(IPromptItem).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IPromptItem>()
                .ToList();

            prompts.ForEach(x =>
            {
                string promptName = x.GetType().ReflectedType.Name;
                _prompts.Add(promptName, new PromptStruct(x.Prompt, x.Instruction));
            });
        }

        public async Task<T> GenerateContentAsync<T>(string key, Dictionary<string, string> promptReplaces)
        {
            var (instruction, prompt) = GetPrompt(key, promptReplaces);
            var response = await GetResponseAsync(instruction, prompt);
            return ProcessContentAsync<T>(response);
        }

        public (string, string) GetPrompt(string key, Dictionary<string, string> promptReplaces)
        {
            string instruction;
            string prompt;
            try
            {
                var promptItem = _prompts[key];
                if (promptItem == null) throw new KeyNotFoundException();

                instruction = promptItem.Instruction;
                prompt = promptItem.Prompt;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            foreach (var (keyReplace, valueReplace) in promptReplaces)
            {
                prompt = prompt.Replace($"__{keyReplace}__", valueReplace);
            }
            return (instruction, prompt);
        }

        public async Task<string> GetResponseAsync(string instruction, string prompt)
        {
            var requestData = new
            {
                system_instruction = new
                {
                    parts = new { text = instruction }
                },
                contents = new
                {
                    parts = new { text = prompt }
                }
            };
            string jsonData = JsonConvert.SerializeObject(requestData);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _httpClient.PostAsync(QueryHelpers.AddQueryString(_geminiUrl, "key", _apiKey), content);
            response.EnsureSuccessStatusCode();

            string responseData = await response.Content.ReadAsStringAsync();
            _logger.LogDebug(responseData);

            try
            {
                var responseDecode = JsonConvert.DeserializeObject<GeminiResponse>(responseData);
                return responseDecode.Candidates[0].Content.Parts[0].Text;
            }
            catch (JsonReaderException e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public T ProcessContentAsync<T>(string response)
        {
            response = response.Trim();

            if (response.StartsWith("```json"))
            {
                response = response.Substring(7);
            }

            if (response.EndsWith("```"))
            {
                response = response.Substring(0, response.Length - 3);
            }

            response = response.Trim();

            try
            {
                T result = JsonConvert.DeserializeObject<T>(response);
                return result == null ? throw new JsonReaderException(response) : result;
            }
            catch (JsonReaderException e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }

    internal record PromptStruct(string Prompt, string Instruction);

    internal class GeminiResponse
    {
        public List<Candidate> Candidates { get; set; }
        public UsageMetadata UsageMetadata { get; set; }
        public string ModelVersion { get; set; }
    }

    public class Candidate
    {
        public Content Content { get; set; }
        public string FinishReason { get; set; }
        public double AvgLogprobs { get; set; }
    }

    public class Content
    {
        public List<Part> Parts { get; set; }
        public string Role { get; set; }
    }

    public class Part
    {
        public string Text { get; set; }
    }

    public class UsageMetadata
    {
        public int PromptTokenCount { get; set; }
        public int CandidatesTokenCount { get; set; }
        public int TotalTokenCount { get; set; }
    }
}
