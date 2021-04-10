namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public abstract class TokenBase<T> : IToken<T>
    {
        public int Position { get; }
        public T Value { get; }

        protected TokenBase(int position, T value)
        {
            Position = position;
            Value = value;
        }
    }
}