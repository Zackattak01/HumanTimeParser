using System;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Extensions
{
    /// <summary>
    /// Set of useful extensions for <see cref="TimeSpan"/>
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Checks if the value of the <see cref="TimeSpan"/> is valid for the given <see cref="ClockType"/>
        /// </summary>
        /// <param name="time">The time to perform the action on</param>
        /// <param name="clockType">The clock type used to judge</param>
        /// <returns>A <see cref="bool"/> that determines if the time is valid</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool IsValidTimeOfDay(this TimeSpan time, ClockType clockType)
        {
            return clockType switch
            {
                ClockType.TwelveHour => time.IsValidTimeOfDay(13), //thirteen because 12:34 is a valid time
                ClockType.TwentyFourHour => time.IsValidTimeOfDay(24),
                _ => throw new ArgumentOutOfRangeException(nameof(clockType), clockType, null)
            };
        }
        
        /// <summary>
        /// CChecks if the value of the <see cref="TimeSpan"/> is valid for the given amount of hours
        /// </summary>
        /// <param name="time">The time to perform the action on</param>
        /// <param name="maxNumberOfHours">The maximum number of hours the time can have.</param>
        /// <returns>A <see cref="bool"/> that determines if the time is valid</returns>
        public static bool IsValidTimeOfDay(this TimeSpan time, int maxNumberOfHours)
        {
            if (time.TotalHours >= maxNumberOfHours || time.TotalSeconds < 1)
                return false;

            return true;
        }
    }
}