namespace HumanTimeParser.Core.TimeConstructs
{
    /// <summary>
    /// Represents a time relative to a given time.
    /// </summary>
    public sealed class RelativeTime
    {
        /// <summary>
        /// The specified format of the relative time.
        /// </summary>
        public RelativeTimeFormat Format { get; }

        /// <summary>
        /// The amount of time.
        /// </summary>
        public double Amount { get; }

        public RelativeTime(double amount, RelativeTimeFormat format)
        {
            Amount = amount;
            Format = format;
        }
    }
}