namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// An unknown token
    /// </summary>
    public class UnknownToken : TokenBase<string>
    {
        /// <inheritdoc/>
        public UnknownToken(int position, int length, string value) : base(position, length, value) { }
    }
}