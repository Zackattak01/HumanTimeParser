using System;
using HumanTimeParser.Core.Extensions;

namespace HumanTimeParser.Core.TimeConstructs
{
    /// <summary>
    /// A data structure to represent a time of day with a specified <see cref="TimePeriod"/>.
    /// </summary>
    public sealed class QualifiedTimeOfDay : TimeOfDay
    {
        /// <summary>
        /// Gets the specified period.
        /// </summary>
        public TimePeriod Period { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="QualifiedTimeOfDay"/> class.
        /// </summary>
        /// <param name="period">The time period</param>
        /// <param name="time">The time</param>
        /// <inheritdoc/>
        public QualifiedTimeOfDay(TimePeriod period, TimeSpan time) : base(time)
        {
            Period = period;
        }

        /// <inheritdoc/>
        public override bool IsValid(ClockType mode = ClockType.TwelveHour)
        {
            return mode switch
            {
                ClockType.TwelveHour => Time.IsValidTimeOfDay(13),
                ClockType.TwentyFourHour => throw  new NotSupportedException("A qualified time of day cannot be judged based on a twenty four hour clock style."),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }
    }
}