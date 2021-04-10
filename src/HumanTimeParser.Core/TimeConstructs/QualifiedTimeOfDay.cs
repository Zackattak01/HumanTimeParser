using System;

namespace HumanTimeParser.Core.TimeConstructs
{
    public sealed class QualifiedTimeOfDay : TimeOfDay
    {
        public TimePeriod Period { get; }
        
        public QualifiedTimeOfDay(TimePeriod period, TimeSpan time) : base(time)
        {
            Period = period;
        }

        public override bool IsValid(ClockType mode = ClockType.TwelveHour)
        {
            return mode switch
            {
                ClockType.TwelveHour => IsValid(12),
                ClockType.TwentyFourHour => throw  new NotSupportedException("A qualified time of day cannot be judged based on a twenty four hour clock style."),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null) //resharper on crack? dont know
            };
        }
    }
}