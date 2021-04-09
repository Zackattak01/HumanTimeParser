namespace HumanTimeParser.Core.TimeConstructs
{
    public sealed class RelativeTime
    {
        public RelativeTimeFormat Format { get; }

        public double Amount { get; }

        public RelativeTime(double amount, RelativeTimeFormat format)
        {
            Amount = amount;
            Format = format;
        }
    }
}