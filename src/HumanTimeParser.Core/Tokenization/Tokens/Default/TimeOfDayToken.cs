using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a <see cref="TimeOfDay"/>
    /// </summary>
    public sealed class TimeOfDayToken : TokenBase<TimeOfDay>
    {
        /// <inheritdoc/>
        public TimeOfDayToken(int position, TimeOfDay value) : base(position, value) { }
    }
}