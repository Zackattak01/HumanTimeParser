using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public class DayOfWeekToken : TokenBase<DayOfWeek>
    {
        public DayOfWeekToken(int position, DayOfWeek value) : base(position, value) { }
    }
}