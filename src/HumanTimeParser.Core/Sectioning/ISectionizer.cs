namespace HumanTimeParser.Core.Sectioning
{
    public interface ISectionizer
    {
        public string[] Sectionize(string input);

        public string NextSection();

        public string PeekNextSection();
    }
}