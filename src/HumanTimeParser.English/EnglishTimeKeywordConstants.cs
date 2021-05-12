using System;
using System.Collections.Generic;
using HumanTimeParser.Core.Extensions;
using HumanTimeParser.Core.TimeConstructs;
using HumanTimeParser.English.Extensions;

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

        public static readonly IReadOnlyDictionary<string, DayOfWeek> DayOfWeekKeywordDictionary;
        public static readonly IReadOnlyDictionary<string, RelativeTimeFormat> RelativeTimeKeywordDictionary;
        static EnglishTimeKeywordConstants()
        {
            var tempDayOfWeekDict = new Dictionary<string, DayOfWeek>(StringComparer.OrdinalIgnoreCase);

            tempDayOfWeekDict.AddKeyRange(MondayAbbreviations, DayOfWeek.Monday);
            tempDayOfWeekDict.AddKeyRange(TuesdayAbbreviations, DayOfWeek.Tuesday);
            tempDayOfWeekDict.AddKeyRange(WednesdayAbbreviations, DayOfWeek.Wednesday);
            tempDayOfWeekDict.AddKeyRange(ThursdayAbbreviations, DayOfWeek.Thursday);
            tempDayOfWeekDict.AddKeyRange(FridayAbbreviations, DayOfWeek.Friday);
            tempDayOfWeekDict.AddKeyRange(SaturdayAbbreviations, DayOfWeek.Saturday);
            tempDayOfWeekDict.AddKeyRange(SundayAbbreviations, DayOfWeek.Sunday);

            DayOfWeekKeywordDictionary = tempDayOfWeekDict;
            
            var tempRelativeTimeAbbreviationsDict = new Dictionary<string, RelativeTimeFormat>(StringComparer.OrdinalIgnoreCase);
            
            tempRelativeTimeAbbreviationsDict.AddKeyRange(TomorrowAbbreviations, RelativeTimeFormat.Tomorrow);
            tempRelativeTimeAbbreviationsDict.AddKeyRange(SecondAbbreviations, RelativeTimeFormat.Seconds);
            tempRelativeTimeAbbreviationsDict.AddKeyRange(MinuteAbbreviations, RelativeTimeFormat.Minutes);
            tempRelativeTimeAbbreviationsDict.AddKeyRange(HourAbbreviations, RelativeTimeFormat.Hours);
            tempRelativeTimeAbbreviationsDict.AddKeyRange(DayAbbreviations, RelativeTimeFormat.Days);
            tempRelativeTimeAbbreviationsDict.AddKeyRange(WeekAbbreviations, RelativeTimeFormat.Weeks);
            tempRelativeTimeAbbreviationsDict.AddKeyRange(MonthAbbreviations, RelativeTimeFormat.Months);
            tempRelativeTimeAbbreviationsDict.AddKeyRange(YearAbbreviations, RelativeTimeFormat.Years);

            RelativeTimeKeywordDictionary = tempRelativeTimeAbbreviationsDict;

        }
    }
}