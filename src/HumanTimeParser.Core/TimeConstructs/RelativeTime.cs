namespace HumanTimeParser.Core.TimeConstructs
{
    public sealed class RelativeTime
    {
        public RelativeTimeFormat Format { get; }

        public double Value { get; }

        public RelativeTime(double value, RelativeTimeFormat format)
        {
            Value = value;
            Format = format;
        }
    }
}