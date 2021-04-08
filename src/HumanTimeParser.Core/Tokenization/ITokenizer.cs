using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Tokenization
{
    public interface ITokenizer
    {
        public void SetInput(string input);

        public IToken NextToken();

        public IToken PeekNextToken();
    }
}