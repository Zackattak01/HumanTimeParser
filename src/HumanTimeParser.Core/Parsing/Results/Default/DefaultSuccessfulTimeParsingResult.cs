using System;

namespace HumanTimeParser.Core.Parsing
{
    public sealed class DefaultSuccessfulTimeParsingResult : ISuccessfulTimeParsingResult<DateTime>
    {
        public DateTime Value { get; }

        public DefaultSuccessfulTimeParsingResult(DateTime value)
        {
            Value = value;
        }
    }
}