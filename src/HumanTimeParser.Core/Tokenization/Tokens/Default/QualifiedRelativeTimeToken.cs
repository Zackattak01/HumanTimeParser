using System.Collections.Generic;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a <see cref="RelativeTime"/>
    /// </summary>
    public sealed class QualifiedRelativeTimeToken : TokenBase<IReadOnlyList<RelativeTime>>
    {
        /// <inheritdoc/>
        public QualifiedRelativeTimeToken(int position, int length, IReadOnlyList<RelativeTime> values) : base(position, length, values) { }
    }
}