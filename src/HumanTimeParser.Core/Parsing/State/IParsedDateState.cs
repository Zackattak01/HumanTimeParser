using System;

namespace HumanTimeParser.Core.Parsing.State
{
    /// <summary>
    /// Represents the state required for parsing a date
    /// </summary>
    public interface IParsedDateState
    {
        /// <summary>
        /// Whether or not a qualified day token was parsed
        /// </summary>
        public bool ParsedQualifiedDay { get; set; }
        
        /// <summary>
        /// The last parsed token index related to the date parsing
        /// </summary>
        public int LastParsedTokenPosition { get; set; }
        
        /// <summary>
        /// The parsed day of the month, null if none
        /// </summary>
        public int? Day { get; set; }
        
        /// <summary>
        /// The parsed month, null if none
        /// </summary>
        public int? Month { get; set; }
        
        /// <summary>
        /// The parsed year, null if none
        /// </summary>
        public int? Year { get; set; }
        
        /// <summary>
        /// The parsed full <see cref="DateTime"/>, null if none
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Checks if the state can build a valid date
        /// </summary>
        /// <returns>A <see cref="bool"/> that determines if a date can be constructed</returns>
        public bool CanConstructValidDate();
    }
}