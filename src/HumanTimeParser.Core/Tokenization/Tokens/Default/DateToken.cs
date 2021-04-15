using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// Represents a date token.
    /// </summary>
    public sealed class DateToken : TokenBase<DateTime>
    {
        /// <inheritdoc/>
        public DateToken(int position, DateTime value) : base(position, value) { }
    }
}