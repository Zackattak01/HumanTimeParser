namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <summary>
    /// Represents a basic token
    /// </summary>
    public interface IToken
    {
        /// <summary>
        /// The position of this token in the input string
        /// </summary>
        public int Position { get; }
        
        /// <summary>
        /// The length of this token in the input string
        /// </summary>
        public int Length { get; }
    }

    /// <summary>
    /// Represents a basic token with a value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IToken<out T> : IToken
    {
        /// <summary>
        /// The value of this token
        /// </summary>
        public T Value { get; }
    }
}