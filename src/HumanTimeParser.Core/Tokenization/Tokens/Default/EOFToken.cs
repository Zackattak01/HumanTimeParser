using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// A token representing the end of the text.
    /// </summary>
    public sealed class EOFToken : IToken
    {
        /// <summary>
        /// This property is not supported on <see cref="EOFToken"/>
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public int Position =>
            throw new NotSupportedException($"{typeof(EOFToken)} does not support {nameof(Position)}");
    }
}