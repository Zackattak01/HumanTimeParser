using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a <see cref="QualifiedTimeOfDay"/>
    /// </summary>
    public sealed class QualifiedTimeOfDayToken : TokenBase<QualifiedTimeOfDay>
    {
        /// <inheritdoc/>
        public QualifiedTimeOfDayToken(int position, QualifiedTimeOfDay value) : base(position, value) { }
    }
}