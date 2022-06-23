namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a day of the month
    /// </summary>
    public class DayOfMonthToken : NumberToken
    {
        /// <inheritdoc/>
        public DayOfMonthToken(int position, int length, int value) : base(position, length, value) { }
    }
}