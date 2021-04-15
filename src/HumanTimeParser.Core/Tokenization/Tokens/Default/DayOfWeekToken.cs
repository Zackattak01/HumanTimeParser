using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// Represents a day of the week token
    /// </summary>
    public class DayOfWeekToken : TokenBase<DayOfWeek>
    {
        /// <inheritdoc/>
        public DayOfWeekToken(int position, DayOfWeek value) : base(position, value) { }
    }
}