using System;

namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class EOFToken : IToken
    {
        public int Position =>
            throw new NotSupportedException($"{typeof(EOFToken)} does not support {nameof(Position)}");
    }
}