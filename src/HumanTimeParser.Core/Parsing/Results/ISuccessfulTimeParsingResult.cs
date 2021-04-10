namespace HumanTimeParser.Core.Parsing
{
    public interface ISuccessfulTimeParsingResult : ITimeParsingResult
    {
        public int FirstParsedTokenPosition { get; }
        public int LastParsedTokenPosition { get; }
    }
    
    public interface ISuccessfulTimeParsingResult<out T> : ISuccessfulTimeParsingResult
    {
        public T Value { get; }
    }
}