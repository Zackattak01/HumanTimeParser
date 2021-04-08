using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class FullyQualifiedRelativeTime : TokenBase<RelativeTime>
    {
        public FullyQualifiedRelativeTime(int position, RelativeTime value) : base(position, value) { }
    }
}