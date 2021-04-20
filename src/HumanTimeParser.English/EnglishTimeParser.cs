using HumanTimeParser.Core.Parsing;
using HumanTimeParser.Core.Parsing.Default;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.English
{
    /// <summary>
    /// An english time parser.
    /// </summary>
    public class EnglishTimeTimeParser : DefaultTimeParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishTimeTimeParser"/> class.
        /// </summary>
        /// <param name="clockType">Sets the clock type to be used.</param>
        /// <param name="input">Sets the input to be parsed.</param>
        public EnglishTimeTimeParser(ClockType clockType = ClockType.TwelveHour) : base(clockType, new EnglishTimeTokenizer(clockType)) { }
        
    }
}