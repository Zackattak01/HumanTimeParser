using System.Globalization;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Culture
{
    /// <summary>
    /// An default implementation of <see cref="ITimeParsingCulture"/>
    /// </summary>
    public class TimeParsingCulture : ITimeParsingCulture
    {
        /// <summary>
        /// A default <see cref="TimeParsingCulture"/>
        /// </summary>
        public static readonly TimeParsingCulture DefaultInstance =
            new TimeParsingCulture(CultureInfo.CurrentCulture, ClockType.TwelveHour);

        /// <inheritdoc/>
        public CultureInfo CultureInfo { get; }

        /// <inheritdoc/>
        public ClockType ClockType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeParsingCulture"/> class.
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <param name="clockType"></param>
        public TimeParsingCulture(CultureInfo cultureInfo, ClockType clockType)
        {
            CultureInfo = cultureInfo;
            ClockType = clockType;
        }
    }
}