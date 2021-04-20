using System;
using System.Collections.Generic;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Parsing.State
{
    public class DefaultTimeParserState : ITimeParserState
    {
        public HashSet<RelativeTimeFormat> ParsedRelativeTimeFormats { get; }
        public List<Func<DateTime, DateTime>> RelativeTimeFunctions { get; }
        public DateTime StartingDate { get; set; }
        public DateTime? ParsedDate { get; set; }
        public TimeSpan? ParsedTime { get; set; }
        public DayOfWeek? ParsedDayOfWeek { get; set; }
        public int FirstParsedTokenPosition { get; set; }
        public int LastParsedTokenPosition { get; set; }

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