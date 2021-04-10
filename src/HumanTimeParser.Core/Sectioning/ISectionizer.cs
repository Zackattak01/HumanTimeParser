namespace HumanTimeParser.Core.Sectioning
{
    public interface ISectionizer
    {
        public Section NextSection();

        public Section PeekNextSection();

        public void SkipSection();
    }
}