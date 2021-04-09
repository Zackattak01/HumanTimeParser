using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.Tokenization;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.English
{
    public class EnglishTimeTokenizer : TokenizerBase
    {
        public EnglishTimeTokenizer(ISectionizer sectionizer) : base(sectionizer) { }
        protected override IToken Tokenize(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}