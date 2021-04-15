using System;

namespace HumanTimeParser.Core.Parsing
{
    /// <summary>
    /// Represents a default successful time parsing result
    /// </summary>
    public sealed class DefaultSuccessfulTimeParsingResult : ISuccessfulTimeParsingResult<DateTime>
    {
        /// <inheritdoc/>
        public DateTime Value { get; }

        /// <inheritdoc/>
        public int FirstParsedTokenIndex { get; }

        /// <inheritdoc/>
        public int LastParsedTokenIndex { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSuccessfulTimeParsingResult"/> class.
        /// </summary>
        /// <param name="value">The value of the time parsing operation</param>
        /// <param name="firstParsedTokenIndex">The index of the first token that was parsed.</param>
        /// <param name="lastParsedTokenIndex">The index of the last token that was parsed.</param>
        public DefaultSuccessfulTimeParsingResult(DateTime value, int firstParsedTokenIndex, int lastParsedTokenIndex)
        {
            Value = value;
            FirstParsedTokenIndex = firstParsedTokenIndex;
            LastParsedTokenIndex = lastParsedTokenIndex;
        }
    }
}