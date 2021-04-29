namespace HumanTimeParser.Core.Tokenization.Tokens
{
    public class UnknownToken : TokenBase<string>
    {
        public UnknownToken(int position, string value) : base(position, value) { }
    }
}