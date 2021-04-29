namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// Represents a basic number token
    /// </summary>
    public sealed class NumberToken : TokenBase<double>
    {
        /// <inheritdoc/>
        public NumberToken(int position, int length, double value) : base(position, length, value) { }
    }
}