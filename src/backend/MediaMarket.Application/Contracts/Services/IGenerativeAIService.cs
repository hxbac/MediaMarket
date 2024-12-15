namespace MediaMarket.Application.Contracts.Services
{
    public interface IGenerativeAIService
    {
        void LoadPrompt();
        (string, string) GetPrompt(string prompt, Dictionary<string, string> promptReplaces);
        Task<string> GetResponseAsync(string instruction, string prompt);
        T ProcessContentAsync<T>(string response);
        Task<T> GenerateContentAsync<T>(string key, Dictionary<string, string> promptReplaces);
    }
}
