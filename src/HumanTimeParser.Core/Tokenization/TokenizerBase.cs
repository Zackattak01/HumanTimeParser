using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Tokenization
{
    public abstract class TokenizerBase : ITokenizer
    {
        protected ISectionizer Sectionizer { get; }
        
        public TokenizerBase(ISectionizer sectionizer)
        {
            Sectionizer = sectionizer;
        }

        public virtual IToken NextToken() => TokenizeSection(Sectionizer.NextSection());
        public virtual IToken PeekNextToken() => TokenizeSection(Sectionizer.PeekNextSection());
        public void SkipToken() => Sectionizer.SkipSection();

        protected abstract IToken TokenizeSection(Section section);
    }
}