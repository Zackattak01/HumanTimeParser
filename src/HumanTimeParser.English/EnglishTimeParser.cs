using HumanTimeParser.Core.Culture;
using HumanTimeParser.Core.Parsing.Default;

namespace HumanTimeParser.English
{
    /// <summary>
    /// An english time parser.
    /// </summary>
    public class EnglishTimeParser : DefaultTimeParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishTimeParser"/> class.
        /// </summary>
        /// <param name="cultureInfo">Sets the clock type to be used.</param>
        public EnglishTimeParser(TimeParsingCulture cultureInfo = null)
            : base(cultureInfo ?? TimeParsingCulture.DefaultInstance,
                new EnglishTimeTokenizer(cultureInfo ?? TimeParsingCulture.DefaultInstance)) { }
        
    }
}