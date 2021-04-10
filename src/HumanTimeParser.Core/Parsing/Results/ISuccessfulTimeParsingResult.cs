namespace HumanTimeParser.Core.Parsing
{
    public interface ISuccessfulTimeParsingResult : ITimeParsingResult
    {
        
    }
    
    public interface ISuccessfulTimeParsingResult<out T> : ISuccessfulTimeParsingResult
    {
        public T Value { get; }
    }
}