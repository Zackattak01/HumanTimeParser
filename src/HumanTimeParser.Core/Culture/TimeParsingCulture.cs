using System.Globalization;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Culture
{
    public class TimeParsingCulture : ITimeParsingCulture
    {
        public static TimeParsingCulture DefaultInstance =
            new TimeParsingCulture(CultureInfo.CurrentCulture, ClockType.TwelveHour);
        
        public CultureInfo CultureInfo { get; }

        public ClockType ClockType { get; }

        public TimeParsingCulture(CultureInfo cultureInfo, ClockType clockType)
        {
            CultureInfo = cultureInfo;
            ClockType = clockType;
        }
    }
}