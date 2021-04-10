using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class TimeOfDayToken : TokenBase<TimeOfDay>
    {
        public TimeOfDayToken(int position, TimeOfDay value) : base(position, value) { }
    }
}