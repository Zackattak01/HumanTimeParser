namespace HumanTimeParser.Core.Sectioning
{
    public class Section
    {
        public int Position { get; }

        public string Value { get; }

        public Section(int position, string value)
        {
            Position = position;
            Value = value;
        }
    }
}