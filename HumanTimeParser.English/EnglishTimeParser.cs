using HumanTimeParser.Core.Parsing;
using HumanTimeParser.Core.Parsing.Default;
using HumanTimeParser.Core.Sectioning;

namespace HumanTimeParser.English
{
    public class EnglishTimeParser : DefaultParser
    {
        public EnglishTimeParser(string input) : base(new EnglishTimeTokenizer(new DefaultSectionizer(input))) { }

        public static ITimeParsingResult Parse(string input)
        {
            return new EnglishTimeParser(input).Parse();
        }
    }
}