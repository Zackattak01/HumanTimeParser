namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public interface IToken
    {
        public int Position { get; }
    }

    public interface IToken<out T> : IToken
    {
        public T Value { get; }
    }
}