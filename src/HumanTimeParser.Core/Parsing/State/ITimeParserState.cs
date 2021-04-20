using System;
using System.Collections.Generic;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Parsing.State
{
    /// <summary>
    /// Represents a basic object for time parser state.
    /// </summary>
    public interface ITimeParserState
    {
        public HashSet<RelativeTimeFormat> ParsedRelativeTimeFormats { get; }
        public List<Func<DateTime, DateTime>> RelativeTimeFunctions { get; }

        public DateTime StartingDate { get; set; }
        public DateTime? ParsedDate { get; set; }
        
        public TimeSpan? ParsedTime { get; set; }
        
        public DayOfWeek? ParsedDayOfWeek { get; set; }

        public int FirstParsedTokenPosition { get; set; }
        public int LastParsedTokenPosition { get; set; }
    }
}