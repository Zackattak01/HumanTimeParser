namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// Represents a basic token
    /// </summary>
    public interface IToken
    {
        public int Position { get; }
    }

    /// <summary>
    /// Represents a basic token with a value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IToken<out T> : IToken
    {
        public T Value { get; }
    }
}