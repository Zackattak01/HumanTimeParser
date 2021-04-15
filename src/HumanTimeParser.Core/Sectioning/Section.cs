namespace HumanTimeParser.Core.Sectioning
{
    /// <summary>
    /// Represents a section of text reading for tokenizing
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Gets the position of this section in the text.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Gets the raw value of this section
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        /// <param name="position">The position in the text of this section.</param>
        /// <param name="value">The raw value of this section.</param>
        public Section(int position, string value)
        {
            Position = position;
            Value = value;
        }
    }
}