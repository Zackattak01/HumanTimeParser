using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a <see cref="QualifiedTimeOfDay"/>
    /// </summary>
    public sealed class QualifiedTimeOfDayToken : TokenBase<QualifiedTimeOfDay>
    {
        /// <inheritdoc/>
        public QualifiedTimeOfDayToken(int position, int length, QualifiedTimeOfDay value) : base(position, length, value) { }
    }
}