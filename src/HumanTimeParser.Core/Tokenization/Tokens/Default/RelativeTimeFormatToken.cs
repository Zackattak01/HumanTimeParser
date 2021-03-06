using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a <see cref="RelativeTimeFormat"/>
    /// </summary>
    public sealed class RelativeTimeFormatToken : TokenBase<RelativeTimeFormat>
    {
        /// <inheritdoc/>
        public RelativeTimeFormatToken(int position, int length, RelativeTimeFormat value) : base(position, length, value) { }
    }
}