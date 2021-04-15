using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a <see cref="RelativeTime"/>
    /// </summary>
    public sealed class QualifiedRelativeTimeToken : TokenBase<RelativeTime>
    {
        public QualifiedRelativeTimeToken(int position, RelativeTime value) : base(position, value) { }
    }
}