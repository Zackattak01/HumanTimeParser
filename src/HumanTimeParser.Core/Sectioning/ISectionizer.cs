namespace HumanTimeParser.Core.Sectioning
{
    public interface ISectionizer
    {
        public string NextSection();

        public string PeekNextSection();
    }
}