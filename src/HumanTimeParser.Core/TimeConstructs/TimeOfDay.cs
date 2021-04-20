using System;

namespace HumanTimeParser.Core.TimeConstructs
{
    /// <summary>
    /// A data structure to represent a time of day.
    /// </summary>
    public class TimeOfDay
    {
        /// <summary>
        /// Gets the time in <see cref="TimeSpan"/> form.
        /// </summary>
        public TimeSpan Time { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeOfDay"/> class.
        /// </summary>
        /// <param name="time"></param>
        public TimeOfDay(TimeSpan time)
        {
            Time = time;
        }

        /// <summary>
        /// Checks if the time represented is valid.
        /// </summary>
        /// <param name="mode">Sets the mode to judge the time by.</param>
        /// <returns>A <see cref="bool"/> that indicates whether the time is valid.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public virtual bool IsValid(ClockType mode = ClockType.TwelveHour)
        {
            return mode switch
            {
                ClockType.TwelveHour => IsValid(13), //thirteen because 12:34 is a valid time
                ClockType.TwentyFourHour => IsValid(24),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null) //resharper on crack? dont know
            };
        }

        protected bool IsValid(int maxNumberOfHours)
        {
            if (Time.TotalHours >= maxNumberOfHours)
                return false;
            if (Time.TotalSeconds < 1)
                return false;

            return true;
        }
        
    }
}