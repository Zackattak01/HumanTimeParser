using System;
using System.Collections;
using System.Collections.Generic;

namespace HumanTimeParser
{
    internal class Constants
    {
        public static readonly IReadOnlyList<string> TomorrowAbbreviations = new List<string> { "tmr", "tomorrow" };
        public static readonly IReadOnlyList<string> SecondAbbreviations = new List<string> { "s", "sec", "secs", "second", "seconds" };
        public static readonly IReadOnlyList<string> MinuteAbbreviations = new List<string> { "m", "min", "mins", "minute", "minutes" };
        public static readonly IReadOnlyList<string> HourAbbreviations = new List<string> { "h", "hr", "hrs", "hour", "hours" };
        public static readonly IReadOnlyList<string> DayAbbreviations = new List<string> { "d", "ds", "day", "days" };
        public static readonly IReadOnlyList<string> WeekAbbreviations = new List<string> { "w", "ws", "week", "weeks" };
        public static readonly IReadOnlyList<string> MonthAbbreviations = new List<string> { "mth", "month", "months" };
        public static readonly IReadOnlyList<string> YearAbbreviations = new List<string> { "y", "ys", "yr", "yrs", "year", "years" };
        public static readonly IReadOnlyList<string> MondayAbbreviations = new List<string> { "mon", "monday" };
        public static readonly IReadOnlyList<string> TuesdayAbbreviations = new List<string> { "tue", "tuesday" };
        public static readonly IReadOnlyList<string> WednesdayAbbreviations = new List<string> { "wed", "wednesday" };
        public static readonly IReadOnlyList<string> ThursdayAbbreviations = new List<string> { "thu", "thursday" };
        public static readonly IReadOnlyList<string> FridayAbbreviations = new List<string> { "fri", "friday" };
        public static readonly IReadOnlyList<string> SaturdayAbbreviations = new List<string> { "sat", "saturday" };
        public static readonly IReadOnlyList<string> SundayAbbreviations = new List<string> { "sun", "sunday" };

        public static readonly IReadOnlyDictionary<TokenType, IReadOnlyList<string>> RelativeTimeAbbreviations = new Dictionary<TokenType, IReadOnlyList<string>>{
        {TokenType.Tomorrow, TomorrowAbbreviations},
        {TokenType.Second, SecondAbbreviations},
        {TokenType.Minute, MinuteAbbreviations},
        {TokenType.Hour, HourAbbreviations},
        {TokenType.Day, DayAbbreviations},
        {TokenType.Week, WeekAbbreviations},
        {TokenType.Month, MonthAbbreviations},
        {TokenType.Year, YearAbbreviations} };

        public static readonly IReadOnlyDictionary<DayOfWeek, IReadOnlyList<string>> WeekdayAbbreviations = new Dictionary<DayOfWeek, IReadOnlyList<string>>{
        {DayOfWeek.Monday, MondayAbbreviations},
        {DayOfWeek.Tuesday, TuesdayAbbreviations},
        {DayOfWeek.Wednesday, WednesdayAbbreviations},
        {DayOfWeek.Thursday, ThursdayAbbreviations},
        {DayOfWeek.Friday, FridayAbbreviations},
        {DayOfWeek.Saturday, SaturdayAbbreviations},
        {DayOfWeek.Sunday, SundayAbbreviations} };
    }
}