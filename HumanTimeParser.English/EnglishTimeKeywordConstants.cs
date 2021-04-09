using System;
using System.Collections.Generic;

namespace HumanTimeParser.English
{
    internal static class EnglishTimeKeywordConstants
    {
        public const string Am = "am";
        public const string Pm = "pm";
        
        public static readonly IReadOnlySet<string> TomorrowAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "tmr", "tomorrow" };
        public static readonly IReadOnlySet<string> SecondAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "s", "sec", "secs", "second", "seconds" };
        public static readonly IReadOnlySet<string> MinuteAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "m", "min", "mins", "minute", "minutes" };
        public static readonly IReadOnlySet<string> HourAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "h", "hr", "hrs", "hour", "hours" };
        public static readonly IReadOnlySet<string> DayAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "d", "ds", "day", "days" };
        public static readonly IReadOnlySet<string> WeekAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "w", "ws", "week", "weeks" };
        public static readonly IReadOnlySet<string> MonthAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "mth", "month", "months" };
        public static readonly IReadOnlySet<string> YearAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "y", "ys", "yr", "yrs", "year", "years" };
        public static readonly IReadOnlySet<string> MondayAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "mon", "monday" };
        public static readonly IReadOnlySet<string> TuesdayAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "tue", "tuesday" };
        public static readonly IReadOnlySet<string> WednesdayAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "wed", "wednesday" };
        public static readonly IReadOnlySet<string> ThursdayAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "thu", "thursday" };
        public static readonly IReadOnlySet<string> FridayAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "fri", "friday" };
        public static readonly IReadOnlySet<string> SaturdayAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "sat", "saturday" };
        public static readonly IReadOnlySet<string> SundayAbbreviations = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "sun", "sunday" };

        // public static readonly IReadOnlyDictionary<TokenType, IReadOnlySet<string>> RelativeTimeAbbreviations = new Dictionary<TokenType, IReadOnlySet<string>>{
        // {.Tomorrow, TomorrowAbbreviations},
        // {TokenType.Second, SecondAbbreviations},
        // {TokenType.Minute, MinuteAbbreviations},
        // {TokenType.Hour, HourAbbreviations},
        // {TokenType.Day, DayAbbreviations},
        // {TokenType.Week, WeekAbbreviations},
        // {TokenType.Month, MonthAbbreviations},
        // {TokenType.Year, YearAbbreviations} };

        // public static readonly IReadOnlyDictionary<DayOfWeek, IReadOnlySet<string>> WeekdayAbbreviations = new Dictionary<DayOfWeek, IReadOnlySet<string>>{
        // {DayOfWeek.Monday, MondayAbbreviations},
        // {DayOfWeek.Tuesday, TuesdayAbbreviations},
        // {DayOfWeek.Wednesday, WednesdayAbbreviations},
        // {DayOfWeek.Thursday, ThursdayAbbreviations},
        // {DayOfWeek.Friday, FridayAbbreviations},
        // {DayOfWeek.Saturday, SaturdayAbbreviations},
        // {DayOfWeek.Sunday, SundayAbbreviations} };
    }
}