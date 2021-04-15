using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a specified <see cref="TimePeriod"/> 
    /// </summary>
    public sealed class PeriodSpecifierToken : TokenBase<TimePeriod>
    {
        /// <inheritdoc/>
        public PeriodSpecifierToken(int position, TimePeriod value) : base(position, value) { }
    }
}