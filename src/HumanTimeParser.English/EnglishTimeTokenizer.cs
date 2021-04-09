using System;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.Tokenization;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.English
{
    public class EnglishTimeTokenizer : TokenizerBase
    {
        public EnglishTimeTokenizer(ISectionizer sectionizer) : base(sectionizer) { }
        protected override IToken TokenizeSection(Section section)
        {
            if (section is null)
                return new EOFToken();
            
            var span = section.Value.AsSpan();

            if (double.TryParse(span, out var number))
                return new NumberToken(section.Position, number);


            return null; //no token was found. return null to let TokenizerBase to try again
        }
    }
}