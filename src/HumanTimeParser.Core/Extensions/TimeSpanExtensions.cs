using System;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        public static bool IsValidTimeOfDay(this TimeSpan time, ClockType clockType)
        {
            return clockType switch
            {
                ClockType.TwelveHour => time.IsValidTimeOfDay(13), //thirteen because 12:34 is a valid time
                ClockType.TwentyFourHour => time.IsValidTimeOfDay(24),
                _ => throw new ArgumentOutOfRangeException(nameof(clockType), clockType, null)
            };
        }
        
        public static bool IsValidTimeOfDay(this TimeSpan time, int maxNumberOfHours)
        {
            if (time.TotalHours >= maxNumberOfHours || time.TotalSeconds < 1)
                return false;

            return true;
        }
    }
}