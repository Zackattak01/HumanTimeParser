namespace HumanTimeParser.Core.Parsing
{
    /// <summary>
    /// Represents a generic time parser.
    /// </summary>
    public interface ITimeParser
    {
        /// <summary>
        /// Parses the input.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <returns>A result indicating success or failure.</returns>
        public ITimeParsingResult Parse(string input);
    }
}