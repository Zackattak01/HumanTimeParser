using System;

namespace HumanTimeParser.Core.TimeConstructs
{
    public class TimeOfDay
    {
        public TimeSpan Value { get; }

        public TimeOfDay(TimeSpan value)
        {
            Value = value;
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
            if (Value.TotalHours >= maxNumberOfHours)
                return false;
            if (Value.TotalSeconds < 1)
                return false;

            return true;
        }
        
    }
}