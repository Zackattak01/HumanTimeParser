using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing a <see cref="DayOfWeek"/>
    /// </summary>
    public class DayOfWeekToken : TokenBase<DayOfWeek>
    {
        /// <inheritdoc/>
        public DayOfWeekToken(int position, int length, DayOfWeek value) : base(position, length, value) { }
    }
}