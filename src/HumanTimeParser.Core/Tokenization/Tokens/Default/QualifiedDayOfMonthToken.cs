namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a qualified day of the month
    /// </summary>
    public class QualifiedDayOfMonthToken : TokenBase<int>
    {
        /// <inheritdoc/>
        public QualifiedDayOfMonthToken(int position, int length, int value) : base(position, length, value) { }
    }
}