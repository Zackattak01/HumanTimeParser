using System;
using System.Collections.Generic;
using HumanTimeParser.Core.Extensions;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.English
{
    internal static class EnglishTimeKeywordConstants
    {
        public const string Am = "am";
        public const string Pm = "pm";
        
        public static readonly IReadOnlySet<string> TomorrowKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "tmr", "tomorrow" };
        public static readonly IReadOnlySet<string> SecondKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "s", "sec", "secs", "second", "seconds" };
        public static readonly IReadOnlySet<string> MinuteKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "m", "min", "mins", "minute", "minutes" };
        public static readonly IReadOnlySet<string> HourKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "h", "hr", "hrs", "hour", "hours" };
        public static readonly IReadOnlySet<string> DayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "d", "ds", "day", "days" };
        public static readonly IReadOnlySet<string> WeekKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "w", "ws", "week", "weeks" };
        public static readonly IReadOnlySet<string> MonthKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "mth", "month", "months" };
        public static readonly IReadOnlySet<string> YearKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "y", "ys", "yr", "yrs", "year", "years" };
        public static readonly IReadOnlySet<string> MondayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "mon", "monday" };
        public static readonly IReadOnlySet<string> TuesdayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "tue", "tuesday" };
        public static readonly IReadOnlySet<string> WednesdayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "wed", "wednesday" };
        public static readonly IReadOnlySet<string> ThursdayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "thu", "thursday" };
        public static readonly IReadOnlySet<string> FridayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "fri", "friday" };
        public static readonly IReadOnlySet<string> SaturdayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "sat", "saturday" };
        public static readonly IReadOnlySet<string> SundayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "sun", "sunday" };
        
        public static readonly IReadOnlySet<string> JanuaryKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "jan", "january" };
        public static readonly IReadOnlySet<string> FebruaryKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "feb", "february" };
        public static readonly IReadOnlySet<string> MarchKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "mar", "march" };
        public static readonly IReadOnlySet<string> AprilKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "apr", "april" };
        public static readonly IReadOnlySet<string> MayKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "may" };
        public static readonly IReadOnlySet<string> JuneKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "jun", "june" };
        public static readonly IReadOnlySet<string> JulyKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "jul", "july" };
        public static readonly IReadOnlySet<string> AugustKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "aug", "august" };
        public static readonly IReadOnlySet<string> SeptemberKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "sep", "sept", "september" };
        public static readonly IReadOnlySet<string> OctoberKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "oct", "october" };
        public static readonly IReadOnlySet<string> NovemberKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "nov", "november" };
        public static readonly IReadOnlySet<string> DecemberKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "dec", "december" };

        public static readonly IReadOnlySet<string> OrdinalNumberIndicators = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "st", "nd", "rd", "th" }; 

        public static readonly IReadOnlyDictionary<string, DayOfWeek> DayOfWeekKeywordDictionary;
        public static readonly IReadOnlyDictionary<string, RelativeTimeFormat> RelativeTimeKeywordDictionary;
        public static readonly IReadOnlyDictionary<string, int> MonthKeywordDictionary;

        static EnglishTimeKeywordConstants()
        {
            var tempDayOfWeekDict = new Dictionary<string, DayOfWeek>(StringComparer.OrdinalIgnoreCase);

            tempDayOfWeekDict.AddKeyRange(MondayKeywords, DayOfWeek.Monday);
            tempDayOfWeekDict.AddKeyRange(TuesdayKeywords, DayOfWeek.Tuesday);
            tempDayOfWeekDict.AddKeyRange(WednesdayKeywords, DayOfWeek.Wednesday);
            tempDayOfWeekDict.AddKeyRange(ThursdayKeywords, DayOfWeek.Thursday);
            tempDayOfWeekDict.AddKeyRange(FridayKeywords, DayOfWeek.Friday);
            tempDayOfWeekDict.AddKeyRange(SaturdayKeywords, DayOfWeek.Saturday);
            tempDayOfWeekDict.AddKeyRange(SundayKeywords, DayOfWeek.Sunday);

            DayOfWeekKeywordDictionary = tempDayOfWeekDict;
            
            var tempRelativeTimeKeywordsDict = new Dictionary<string, RelativeTimeFormat>(StringComparer.OrdinalIgnoreCase);
            
            tempRelativeTimeKeywordsDict.AddKeyRange(TomorrowKeywords, RelativeTimeFormat.Tomorrow);
            tempRelativeTimeKeywordsDict.AddKeyRange(SecondKeywords, RelativeTimeFormat.Seconds);
            tempRelativeTimeKeywordsDict.AddKeyRange(MinuteKeywords, RelativeTimeFormat.Minutes);
            tempRelativeTimeKeywordsDict.AddKeyRange(HourKeywords, RelativeTimeFormat.Hours);
            tempRelativeTimeKeywordsDict.AddKeyRange(DayKeywords, RelativeTimeFormat.Days);
            tempRelativeTimeKeywordsDict.AddKeyRange(WeekKeywords, RelativeTimeFormat.Weeks);
            tempRelativeTimeKeywordsDict.AddKeyRange(MonthKeywords, RelativeTimeFormat.Months);
            tempRelativeTimeKeywordsDict.AddKeyRange(YearKeywords, RelativeTimeFormat.Years);

            RelativeTimeKeywordDictionary = tempRelativeTimeKeywordsDict;

            var tempMonthKeywordDictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            
            tempMonthKeywordDictionary.AddKeyRange(JanuaryKeywords, 1);
            tempMonthKeywordDictionary.AddKeyRange(FebruaryKeywords, 2);
            tempMonthKeywordDictionary.AddKeyRange(MarchKeywords, 3);
            tempMonthKeywordDictionary.AddKeyRange(AprilKeywords, 4);
            tempMonthKeywordDictionary.AddKeyRange(MayKeywords, 5);
            tempMonthKeywordDictionary.AddKeyRange(JuneKeywords, 6);
            tempMonthKeywordDictionary.AddKeyRange(JulyKeywords, 7);
            tempMonthKeywordDictionary.AddKeyRange(AugustKeywords, 8);
            tempMonthKeywordDictionary.AddKeyRange(SeptemberKeywords, 9);
            tempMonthKeywordDictionary.AddKeyRange(OctoberKeywords, 10);
            tempMonthKeywordDictionary.AddKeyRange(NovemberKeywords, 11);
            tempMonthKeywordDictionary.AddKeyRange(DecemberKeywords, 12);

            MonthKeywordDictionary = tempMonthKeywordDictionary;

        }
    }
}