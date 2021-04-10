using System;

namespace HumanTimeParser.Core.Parsing
{
    public sealed class DefaultSuccessfulTimeParsingResult : ISuccessfulTimeParsingResult<DateTime>
    {
        public DateTime Value { get; }

        public int FirstParsedTokenPosition { get; }
        public int LastParsedTokenPosition { get; }


        public DefaultSuccessfulTimeParsingResult(DateTime value, int firstParsedTokenPosition, int lastParsedTokenPosition)
        {
            Value = value;
            FirstParsedTokenPosition = firstParsedTokenPosition;
            LastParsedTokenPosition = lastParsedTokenPosition;
        }
    }
}