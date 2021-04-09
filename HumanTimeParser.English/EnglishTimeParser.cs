using HumanTimeParser.Core.Parsing;
using HumanTimeParser.Core.Parsing.Default;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.English
{
    public class EnglishTimeParser : DefaultParser
    {
        public EnglishTimeParser(ClockType clockType, string input) : base(clockType, new EnglishTimeTokenizer(new DefaultSectionizer(input))) { }

        public static ITimeParsingResult Parse(string input, ClockType clockType = ClockType.TwelveHour)
        {
            return new EnglishTimeParser(clockType, input).Parse();
        }
    }
}