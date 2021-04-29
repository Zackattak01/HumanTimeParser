using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// Represents a day of the week token
    /// </summary>
    public class DayOfWeekToken : TokenBase<DayOfWeek>
    {
        /// <inheritdoc/>
        public DayOfWeekToken(int position, int length, DayOfWeek value) : base(position, length, value) { }
    }
}