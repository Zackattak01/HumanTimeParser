using System.Globalization;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Culture
{
    /// <summary>
    /// Represents a culture object used for time parsing
    /// </summary>
    public interface ITimeParsingCulture
    {
        /// <summary>
        /// The <see cref="CultureInfo"/> to be used
        /// </summary>
        public CultureInfo CultureInfo { get; }
        
        /// <summary>
        /// The <see cref="ClockType"/> to be used
        /// </summary>
        public ClockType ClockType { get; }
    }
}