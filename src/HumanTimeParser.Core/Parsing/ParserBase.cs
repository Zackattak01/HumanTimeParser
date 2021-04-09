using HumanTimeParser.Core.Tokenization;

namespace HumanTimeParser.Core.Parsing
{
    public abstract class ParserBase : ITimeParser
    {
        protected ITokenizer Tokenizer { get; }
        
        public ParserBase(ITokenizer tokenizer)
        {
            Tokenizer = tokenizer;
        }
        
        public abstract ITimeParsingResult Parse();
    }
}