using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class QualifiedRelativeTimeToken : TokenBase<RelativeTime>
    {
        public QualifiedRelativeTimeToken(int position, RelativeTime value) : base(position, value) { }
    }
}