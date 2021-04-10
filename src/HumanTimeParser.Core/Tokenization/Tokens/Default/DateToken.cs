using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class DateToken : TokenBase<DateTime>
    {
        public DateToken(int position, DateTime value) : base(position, value) { }
    }
}