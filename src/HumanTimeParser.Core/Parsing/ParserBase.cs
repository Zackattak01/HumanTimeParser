namespace HumanTimeParser.Core.Parsing
{
    public abstract class ParserBase : ITimeParser
    {
        public ParserBase()
        {
            
        }
        
        public abstract ITimeParsingResult Parse(string input);
    }
}