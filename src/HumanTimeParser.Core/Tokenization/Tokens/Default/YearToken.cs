namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a year
    /// </summary>
    public class YearToken : NumberToken
    {
        /// <inheritdoc/>
        public YearToken(int position, int length, int value) : base(position, length, value) { }
    }
}