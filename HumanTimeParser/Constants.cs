using System;
using System.Collections;
using System.Collections.Generic;

namespace HumanTimeParser
{
    public class Constants
    {
        public static readonly IReadOnlyCollection<string> TomorrowAbbreviations = new List<string> { "tmr", "tomorrow" };

        public const string SecondAbbreviation = "s";
        public static readonly IReadOnlyList<string> SecondAbbreviations = new List<string> { "sec", "secs", "second", "seconds" };

        public const string MinuteAbbreviation = "m";
        public static readonly IReadOnlyList<string> MinuteAbbreviations = new List<string> { "min", "mins", "minute", "minutes" };

        public const string HourAbbreviation = "h";
        public static readonly IReadOnlyList<string> HourAbbreviations = new List<string> { "hr", "hrs", "hour", "hours" };

        public const string DayAbbreviation = "d";
        public static readonly IReadOnlyList<string> DayAbbreviations = new List<string> { "ds", "day", "days" };

        public const string WeekAbbreviation = "w";
        public static readonly IReadOnlyList<string> WeekAbbreviations = new List<string> { "ws", "week", "weeks" };

        public const string MonthAbbreviation = "M";
        public static readonly IReadOnlyList<string> MonthAbbreviations = new List<string> { "Ms", "month", "months" };

        public const string YearAbbreviation = "y";
        public static readonly IReadOnlyList<string> YearAbbreviations = new List<string> { "ys", "yr", "yrs", "year", "years" };


    }
}