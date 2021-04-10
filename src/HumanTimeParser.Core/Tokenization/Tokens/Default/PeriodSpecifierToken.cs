using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class PeriodSpecifierToken : TokenBase<TimePeriod>
    {
        public PeriodSpecifierToken(int position, TimePeriod value) : base(position, value) { }
    }
}