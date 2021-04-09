using System;

namespace HumanTimeParser.Core.TimeConstructs
{
    public class TimeOfDay
    {
        public TimeSpan Time { get; }

        public TimeOfDay(TimeSpan time)
        {
            Time = time;
        }

        public virtual bool IsValid(ClockType mode = ClockType.TwelveHour)
        {
            return mode switch
            {
                ClockType.TwelveHour => IsValid(12),
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