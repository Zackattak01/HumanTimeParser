using System;
using System.Collections.Generic;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Parsing.State
{
    /// <inheritdoc/>
    public class DefaultTimeParserState : ITimeParserState
    {
        /// <inheritdoc/>
        public HashSet<RelativeTimeFormat> ParsedRelativeTimeFormats { get; }

        /// <inheritdoc/>
        public List<Func<DateTime, DateTime>> RelativeTimeFunctions { get; }

        /// <inheritdoc/>
        public DateTime StartingDate { get; set; }

        /// <inheritdoc/>
        public DateTime? ParsedDate { get; set; }

        /// <inheritdoc/>
        public TimeSpan? ParsedTime { get; set; }

        /// <inheritdoc/>
        public DayOfWeek? ParsedDayOfWeek { get; set; }

        /// <inheritdoc/>
        public int FirstParsedTokenPosition { get; set; }

        /// <inheritdoc/>
        public int LastParsedTokenPosition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTimeParserState"/> class.
        /// </summary>
        public DefaultTimeParserState()
        {
            ParsedRelativeTimeFormats = new HashSet<RelativeTimeFormat>();
            RelativeTimeFunctions = new List<Func<DateTime, DateTime>>();
         
            StartingDate = DateTime.Now;

            FirstParsedTokenPosition = -1;
            LastParsedTokenPosition = -1;
        }
    }
}