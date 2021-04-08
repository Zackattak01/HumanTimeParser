namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public interface IToken
    {
        
    }

    public interface IToken<out T> : IToken
    {
        //Not included in IToken for EOF Tokens
        public int Position { get; }
        public T Value { get; }
    }
}