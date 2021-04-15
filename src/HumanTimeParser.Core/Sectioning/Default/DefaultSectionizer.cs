using System;

namespace HumanTimeParser.Core.Sectioning
{
    /// <inheritdoc/>
    public sealed class DefaultSectionizer : ISectionizer
    {
        private const string DefaultSplitString = " ";
        
        public string SplitString { get; }
        
        private readonly string[] _sections;

        private int _currentIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSectionizer"/> class.
        /// </summary>
        /// <param name="input">The raw input to sectionize.</param>
        /// <param name="splitOn">The string to split the <see cref="input"/> on/</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DefaultSectionizer(string input, string splitOn = DefaultSplitString)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));
            if (splitOn is null) throw new ArgumentNullException(nameof(splitOn));


            _sections = input.Split(splitOn);
            SplitString = splitOn;
            _currentIndex = -1;
        }

        /// <inheritdoc/>
        public Section NextSection()
        {
            if (++_currentIndex >= _sections.Length)
                return null;

            return new Section(_currentIndex, _sections[_currentIndex]);
        }

        /// <inheritdoc/>
        public Section PeekNextSection()
        {
            var nextSection = NextSection();
            _currentIndex--;

            return nextSection;
        }

        /// <inheritdoc/>
        public void SkipSection()
            => _currentIndex++;
        
    }
}