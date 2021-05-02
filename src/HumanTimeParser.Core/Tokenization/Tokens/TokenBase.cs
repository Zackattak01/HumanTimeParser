namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <inheritdoc/>
    public abstract class TokenBase<T> : IToken<T>
    {
        /// <inheritdoc/>
        public int Position { get; }

        /// <inheritdoc/>
        public int Length { get; }

        /// <inheritdoc/>
        public T Value { get; }
        
        /// <summary>
        /// Initializes a new instance of the base <see cref="TokenBase{T}"/> class.
        /// </summary>
        /// <param name="position">The position of the token</param>
        /// <param name="length">The length of the token</param>
        /// <param name="value">The value of the token</param>
        protected TokenBase(int position, int length, T value)
        {
            Position = position;
            Value = value;
            Length = length;
        }
    }
}