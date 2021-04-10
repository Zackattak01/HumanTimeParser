using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Tokenization
{
    public interface ITokenizer
    {
        public IToken NextToken();

        public IToken PeekNextToken();
        public void SkipToken();
    }
}