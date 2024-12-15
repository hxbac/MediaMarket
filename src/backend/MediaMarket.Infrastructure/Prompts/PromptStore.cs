namespace MediaMarket.Infrastructure.Prompts
{
    public static class PromptStore
    {
        public static class EnhanceProduct
        {
            static string Instruction => @"You are an assistant specializing in creating engaging product descriptions and tags. You receive a JSON input with the following schema:
                {
                    ""productName"": ""<Product Name>"",
                    ""shortDescription"": ""<Short Description>"",
                    ""description"": ""<Description>"",
                    ""contentType"": ""<Image or video>""
                }
                Your task is to:
                Enhance the description field to approximately 500 words, ensuring it is compelling, detailed, and informative. Maintain consistency with the tone and language of the input fields.
                Generate 10 descriptive and relevant tags for the product. Tags should be concise and optimized for search or categorization purposes.
                Your response must be a JSON object with the following structure:
                {
                    ""description"": ""<Enhanced description>"",
                    ""tags"": [""<Tag 1>"", ""<Tag 2>"", ..., ""<Tag 10>""]
                }
            ";

            static string Prompt => @"
                {
                    ""productName"": ""__PRODUCT_NAME__"",
                    ""shortDescription"": ""__SHORT_DESCRIPTION__"",
                    ""description"": ""__DESCRIPTION__"",
                    ""contentType"": ""__CONTENT_TYPE__""
                }
            ";
        }
    }
}
