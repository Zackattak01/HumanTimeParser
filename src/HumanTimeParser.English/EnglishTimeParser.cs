using HumanTimeParser.Core.Parsing;
using HumanTimeParser.Core.Parsing.Default;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.English
{
    /// <summary>
    /// An english time parser.
    /// </summary>
    public class EnglishTimeParser : DefaultParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishTimeParser"/> class.
        /// </summary>
        /// <param name="clockType">Sets the clock type to be used.</param>
        /// <param name="input">Sets the input to be parsed.</param>
        public EnglishTimeParser(ClockType clockType, string input) : base(clockType, new EnglishTimeTokenizer(new DefaultSectionizer(input))) { }

        /// <summary>
        /// A parsing function that handles creating the underlying parser.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <param name="clockType">The clock type to use.</param>
        /// <returns>A basic time <see cref="ITimeParsingResult"/>.</returns>
        public static ITimeParsingResult Parse(string input, ClockType clockType = ClockType.TwelveHour)
        {
            return new EnglishTimeParser(clockType, input).Parse();
        }
    }
}