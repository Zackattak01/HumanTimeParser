namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a number
    /// </summary>
    public class NumberToken : TokenBase<double>
    {
        /// <inheritdoc/>
        public NumberToken(int position, int length, double value) : base(position, length, value) { }
    }
}