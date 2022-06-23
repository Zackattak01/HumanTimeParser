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
        /// <summary>
        /// The <see cref="RelativeTimeFormat"/>s that have already been parsed.
        /// </summary>
        public HashSet<RelativeTimeFormat> ParsedRelativeTimeFormats { get; }
        
        /// <summary>
        /// The functions to be applied when constructing the final <see cref="DateTime"/>
        /// </summary>
        public List<Func<DateTime, DateTime>> RelativeTimeFunctions { get; }

        /// <summary>
        /// The starting date of the parse operation
        /// </summary>
        public DateTime StartingDate { get; set; }
        
        /// <summary>
        /// State representing a parsed date
        /// </summary>
        public IParsedDateState ParsedDateState { get; set; }

        /// <summary>
        /// The time of day that has been parsed from the input; null if one has not been parsed
        /// </summary>
        public TimeSpan? ParsedTime { get; set; }
        
        /// <summary>
        /// The day of week that has been parsed from the input; null if one has not been parsed
        /// </summary>
        public DayOfWeek? ParsedDayOfWeek { get; set; }

        /// <summary>
        /// The index of the first parsed token.
        /// Index based on the length of the input <see cref="string"/>
        /// </summary>
        public int FirstParsedTokenPosition { get; set; }
        
        /// <summary>
        /// The index of the last parsed token.
        /// Index based on the length of the input <see cref="string"/>
        /// </summary>
        public int LastParsedTokenPosition { get; set; }
    }
}