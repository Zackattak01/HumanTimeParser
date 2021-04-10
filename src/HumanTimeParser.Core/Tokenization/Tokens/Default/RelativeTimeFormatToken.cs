using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class RelativeTimeFormatToken : TokenBase<RelativeTimeFormat>
    {
        public RelativeTimeFormatToken(int position, RelativeTimeFormat value) : base(position, value) { }
    }
}