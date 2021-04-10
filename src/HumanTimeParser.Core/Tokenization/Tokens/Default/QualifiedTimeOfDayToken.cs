using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class QualifiedTimeOfDayToken : TokenBase<QualifiedTimeOfDay>
    {
        public QualifiedTimeOfDayToken(int position, QualifiedTimeOfDay value) : base(position, value) { }
    }
}