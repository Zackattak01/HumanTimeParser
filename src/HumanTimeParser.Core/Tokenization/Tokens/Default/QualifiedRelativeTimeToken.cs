using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a <see cref="RelativeTime"/>
    /// </summary>
    public sealed class QualifiedRelativeTimeToken : TokenBase<RelativeTime>
    {
        /// <inheritdoc/>
        public QualifiedRelativeTimeToken(int position, int length, RelativeTime value) : base(position, length, value) { }
    }
}