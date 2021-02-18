using System;

namespace HumanTimeParser
{
    internal class Token
    {
        public TokenType TokenType { get; }
        public string Value { get; }
        public int TokenPosition { get; }

        public Token(TokenType type, int tokenPosition, string value = null)
        {
            TokenType = type;
            TokenPosition = tokenPosition;
            Value = value;
        }
    }
}