using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// Represents a date token.
    /// </summary>
    public sealed class DateToken : TokenBase<DateTime>
    {
        /// <inheritdoc/>
        public DateToken(int position, int length, DateTime value) : base(position, length, value) { }
    }
}