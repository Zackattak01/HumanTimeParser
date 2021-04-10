namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public sealed class NumberToken : TokenBase<double>
    {
        public NumberToken(int position, double value) : base(position, value) { }
    }
}