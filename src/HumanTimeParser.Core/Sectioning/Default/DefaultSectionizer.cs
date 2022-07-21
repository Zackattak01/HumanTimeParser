using System;

namespace HumanTimeParser.Core.Sectioning
{
    /// <inheritdoc/>
    public sealed class DefaultSectionizer : ISectionizer
    {
        private const string DefaultSplitString = " ";
        
        /// <summary>
        /// The <see cref="string"/> that is used for splitting the input
        /// </summary>
        public string SplitString { get; }
        
        private readonly string[] _sections;

        private int _currentIndex;
        private int _currentInputLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSectionizer"/> class.
        /// </summary>
        /// <param name="input">The raw input to sectionize.</param>
        /// <param name="splitOn">The string to split the <paramref name="input"/> on</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DefaultSectionizer(string input, string splitOn = DefaultSplitString)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));
            if (splitOn is null) throw new ArgumentNullException(nameof(splitOn));


            _sections = input.Split(splitOn);
            SplitString = splitOn;
            _currentIndex = -1;
            _currentInputLength = 0;
        }

        private int GetSectionLength(int index) 
            => index == 0 ? 
                _sections[index].Length :
                _sections[index].Length + SplitString.Length; // add one for the separator char which isn't included here

        private Section NextSection(bool peeking)
        {
            var currentIndex = peeking ? _currentIndex + 1 : ++_currentIndex;

            if (currentIndex >= _sections.Length)
                return null;

            var sectionLength = GetSectionLength(currentIndex);
            var sectionPosition = peeking ? _currentInputLength + sectionLength : _currentInputLength += sectionLength;

            return new Section(sectionPosition, _sections[currentIndex]);
        }

        /// <inheritdoc/>
        public Section NextSection()
            => NextSection(false);

        /// <inheritdoc/>
        public Section PeekNextSection()
            => NextSection(true);

        /// <inheritdoc/>
        public void AdvanceSection()
        {
            _currentIndex++;
            _currentInputLength += GetSectionLength(_currentIndex);
        }
    }
}