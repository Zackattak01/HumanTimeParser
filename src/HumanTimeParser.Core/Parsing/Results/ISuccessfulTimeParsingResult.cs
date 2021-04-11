namespace HumanTimeParser.Core.Parsing
{
    public interface ISuccessfulTimeParsingResult : ITimeParsingResult
    {
        public int FirstParsedTokenIndex { get; }
        public int LastParsedTokenIndex { get; }
    }
    
    public interface ISuccessfulTimeParsingResult<out T> : ISuccessfulTimeParsingResult
    {
        public T Value { get; }
    }
}