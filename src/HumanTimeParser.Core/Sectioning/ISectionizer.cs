namespace HumanTimeParser.Core.Sectioning
{
    /// <summary>
    /// Represents a basic sectionizer.  Responsible for splitting strings.  
    /// </summary>
    public interface ISectionizer
    {
        /// <summary>
        /// Advances to the next section.
        /// </summary>
        /// <returns>The next section.</returns>
        public Section NextSection();

        /// <summary>
        /// Looks at the next section without advancing to it.
        /// </summary>
        /// <returns>The next section.</returns>
        public Section PeekNextSection();

        /// <summary>
        /// Moves to the next section, without doing any processing.
        /// </summary>
        public void SkipSection();
    }
}