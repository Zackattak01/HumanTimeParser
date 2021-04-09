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

        public virtual IToken NextToken() => Tokenize(Sectionizer.NextSection());
        public virtual IToken PeekNextToken() => Tokenize(Sectionizer.PeekNextSection());
        
        protected abstract IToken Tokenize(string input);
    }
}