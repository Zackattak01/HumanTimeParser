using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Tokenization
{
    /// <summary>
    /// Represents a basic tokenizer.
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// Gets the current token.
        /// </summary>
        public IToken CurrentToken { get; }
        
        /// <summary>
        /// Advances to the next token.
        /// </summary>
        /// <returns>The next token.</returns>
        public IToken NextToken();

        /// <summary>
        /// Gets the next token without advancing to it.
        /// </summary>
        /// <returns>The next token</returns>
        public IToken PeekNextToken();
        
        /// <summary>
        /// Moves to the next token without doing any processing.
        /// </summary>
        public void SkipToken();
    }
}