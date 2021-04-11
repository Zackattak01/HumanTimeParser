using System;

namespace HumanTimeParser.Core.Parsing
{
    public sealed class DefaultSuccessfulTimeParsingResult : ISuccessfulTimeParsingResult<DateTime>
    {
        public DateTime Value { get; }

        public int FirstParsedTokenIndex { get; }
        public int LastParsedTokenIndex { get; }


        public DefaultSuccessfulTimeParsingResult(DateTime value, int firstParsedTokenPosition, int lastParsedTokenPosition)
        {
            Value = value;
            FirstParsedTokenIndex = firstParsedTokenPosition;
            LastParsedTokenIndex = lastParsedTokenPosition;
        }
    }
}