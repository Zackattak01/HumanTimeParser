using System;
using System.Collections.Generic;

namespace HumanTimeParser
{
    internal class Constants
    {
        public static readonly IReadOnlySet<string> TomorrowAbbreviations = new HashSet<string> { "tmr", "tomorrow" };
        public static readonly IReadOnlySet<string> SecondAbbreviations = new HashSet<string> { "s", "sec", "secs", "second", "seconds" };
        public static readonly IReadOnlySet<string> MinuteAbbreviations = new HashSet<string> { "m", "min", "mins", "minute", "minutes" };
        public static readonly IReadOnlySet<string> HourAbbreviations = new HashSet<string> { "h", "hr", "hrs", "hour", "hours" };
        public static readonly IReadOnlySet<string> DayAbbreviations = new HashSet<string> { "d", "ds", "day", "days" };
        public static readonly IReadOnlySet<string> WeekAbbreviations = new HashSet<string> { "w", "ws", "week", "weeks" };
        public static readonly IReadOnlySet<string> MonthAbbreviations = new HashSet<string> { "mth", "month", "months" };
        public static readonly IReadOnlySet<string> YearAbbreviations = new HashSet<string> { "y", "ys", "yr", "yrs", "year", "years" };
        public static readonly IReadOnlySet<string> MondayAbbreviations = new HashSet<string> { "mon", "monday" };
        public static readonly IReadOnlySet<string> TuesdayAbbreviations = new HashSet<string> { "tue", "tuesday" };
        public static readonly IReadOnlySet<string> WednesdayAbbreviations = new HashSet<string> { "wed", "wednesday" };
        public static readonly IReadOnlySet<string> ThursdayAbbreviations = new HashSet<string> { "thu", "thursday" };
        public static readonly IReadOnlySet<string> FridayAbbreviations = new HashSet<string> { "fri", "friday" };
        public static readonly IReadOnlySet<string> SaturdayAbbreviations = new HashSet<string> { "sat", "saturday" };
        public static readonly IReadOnlySet<string> SundayAbbreviations = new HashSet<string> { "sun", "sunday" };

        public static readonly IReadOnlyDictionary<TokenType, IReadOnlySet<string>> RelativeTimeAbbreviations = new Dictionary<TokenType, IReadOnlySet<string>>{
        {TokenType.Tomorrow, TomorrowAbbreviations},
        {TokenType.Second, SecondAbbreviations},
        {TokenType.Minute, MinuteAbbreviations},
        {TokenType.Hour, HourAbbreviations},
        {TokenType.Day, DayAbbreviations},
        {TokenType.Week, WeekAbbreviations},
        {TokenType.Month, MonthAbbreviations},
        {TokenType.Year, YearAbbreviations} };

        public static readonly IReadOnlyDictionary<DayOfWeek, IReadOnlySet<string>> WeekdayAbbreviations = new Dictionary<DayOfWeek, IReadOnlySet<string>>{
        {DayOfWeek.Monday, MondayAbbreviations},
        {DayOfWeek.Tuesday, TuesdayAbbreviations},
        {DayOfWeek.Wednesday, WednesdayAbbreviations},
        {DayOfWeek.Thursday, ThursdayAbbreviations},
        {DayOfWeek.Friday, FridayAbbreviations},
        {DayOfWeek.Saturday, SaturdayAbbreviations},
        {DayOfWeek.Sunday, SundayAbbreviations} };
    }
}