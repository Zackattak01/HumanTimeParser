namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a month
    /// </summary>
    public sealed class MonthToken : TokenBase<int>
    {
        /// <inheritdoc/>
        public MonthToken(int position, int length, int value) : base(position, length, value) { }
    }
}