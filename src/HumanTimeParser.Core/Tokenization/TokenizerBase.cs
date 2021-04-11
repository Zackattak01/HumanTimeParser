using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Tokenization
{
    public abstract class TokenizerBase : ITokenizer
    {
        public IToken CurrentToken { get; private set; }
        protected ISectionizer Sectionizer { get; }


        public TokenizerBase(ISectionizer sectionizer)
        {
            Sectionizer = sectionizer;
        }

        public virtual IToken NextToken()
        {
            var token = TokenizeSection(Sectionizer.NextSection());
            if (token is null)
                return NextToken();

            CurrentToken = token;
            return token;
        }
        public virtual IToken PeekNextToken()
        {
            var token = TokenizeSection(Sectionizer.PeekNextSection());
            if (token is null)
                return NextToken();

            CurrentToken = token;
            return token;
        }
        public void SkipToken() => Sectionizer.SkipSection();

        protected abstract IToken TokenizeSection(Section section);
    }
}