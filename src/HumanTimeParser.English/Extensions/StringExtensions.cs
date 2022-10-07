using System;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.English.Extensions
{
    internal static class StringExtensions
    {
        public static int FirstNonNumberPos(this string str)
        {
            var chars = str.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (!char.IsDigit(chars[i]) && !char.IsPunctuation(chars[i]))
                    return i;
            }

            return -1;
        }

        public static bool ContainsNumber(this string str)
        {
            foreach (var c in str)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }

        public static bool TryParseTimePeriodSpecifier(this string str, out TimePeriod result)
        {
            if (str.Equals(EnglishTimeKeywordConstants.Am, StringComparison.OrdinalIgnoreCase))
            {
                result = TimePeriod.Am;
                return true;
            }
            else if(str.Equals(EnglishTimeKeywordConstants.Pm, StringComparison.OrdinalIgnoreCase))
            {
                result = TimePeriod.Pm;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }
    }
}