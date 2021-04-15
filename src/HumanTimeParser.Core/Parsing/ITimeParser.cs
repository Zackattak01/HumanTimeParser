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
        /// <returns>A result indicating success or failure.</returns>
        public ITimeParsingResult Parse();
    }
}