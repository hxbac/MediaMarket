namespace MediaMarket.Infrastructure.Prompts
{
    public interface IPromptItem
    {
        public string Instruction { get; }
        public string Prompt { get; }
    }
}
