using System;

namespace HumanTimeParser.Core.Sectioning
{
    public sealed class DefaultSectionizer : ISectionizer
    {
        private const string DefaultSplitString = " ";
        
        public string SplitString { get; }
        
        private string[] _sections;

        private int _currentIndex;

        public DefaultSectionizer(string splitOn = DefaultSplitString)
        {
            SplitString = splitOn;
        }
        
        public string[] Sectionize(string input)
        {
            _sections = input.Split(SplitString);
            _currentIndex = -1;
            return _sections;
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
    }
}