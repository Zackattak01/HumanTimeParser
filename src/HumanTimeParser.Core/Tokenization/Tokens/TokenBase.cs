namespace HumanTimeParser.Core.Tokenization.Tokens
{
    /// <inheritdoc/>
    public abstract class TokenBase<T> : IToken<T>
    {
        /// <inheritdoc/>
        public int Position { get; }

        /// <inheritdoc/>
        public T Value { get; }
        
        /// <summary>
        /// Initializes a new instance of the base <see cref="TokenBase{T}"/> class.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="value"></param>
        protected TokenBase(int position, T value)
        {
            Position = position;
            Value = value;
        }
    }
}