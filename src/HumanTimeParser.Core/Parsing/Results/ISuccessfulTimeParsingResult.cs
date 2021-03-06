namespace HumanTimeParser.Core.Parsing
{
    /// <summary>
    /// Represents a basic successful time parsing result.
    /// </summary>
    public interface ISuccessfulTimeParsingResult : ITimeParsingResult
    {
        /// <summary>
        /// The index of the first parsed token.
        /// Index based on the length of the input <see cref="string"/>
        /// </summary>
        public int FirstParsedTokenIndex { get; }
        /// <summary>
        /// The index of the last parsed token.
        /// Index based on the length of the input <see cref="string"/>
        /// </summary>
        public int LastParsedTokenIndex { get; }
    }
    
    /// <summary>
    /// Represents a successful time parsing result with a parsed value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface ISuccessfulTimeParsingResult<out T> : ISuccessfulTimeParsingResult
    {
        /// <summary>
        /// Gets the parsed value of the parsing result.
        /// </summary>
        public T Value { get; }
    }
}