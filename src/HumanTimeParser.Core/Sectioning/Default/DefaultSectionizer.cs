using System;

namespace HumanTimeParser.Core.Sectioning
{
    public sealed class DefaultSectionizer : ISectionizer
    {
        private const string DefaultSplitString = " ";
        
        public string SplitString { get; }
        
        private readonly string[] _sections;

        private int _currentIndex;

        public DefaultSectionizer(string input, string splitOn = DefaultSplitString)
        {
            _sections = input.Split(splitOn);
            SplitString = splitOn;
            _currentIndex = -1;
        }
        
        public string NextSection()
        {
            if (_sections is null)
                throw new Exception("No input has been provided.");

            if (_currentIndex++ >= _sections.Length)
                return null;

            return _sections[_currentIndex];
        }

        public string PeekNextSection()
        {
            var nextSection = NextSection();
            _currentIndex--;

            return nextSection;
        }

        public void SkipSection()
         => _currentIndex++;
        
    }
}