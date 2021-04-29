using System;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.English.Extensions
{
    public static class SpanExtensions
    {
        public static bool TryParseEndingTimePeriodSpecifier(this ReadOnlySpan<char> input, out TimePeriod result)
        {
            if (!(input.Length >= 2))
            {
                result = default;
                return false;
            }

            var abbreviation = input[^2..];

            if (abbreviation.Equals(EnglishTimeKeywordConstants.Am, StringComparison.OrdinalIgnoreCase))
            {
                result = TimePeriod.Am;
                return true;
            }
            else if (abbreviation.Equals(EnglishTimeKeywordConstants.Pm, StringComparison.OrdinalIgnoreCase))
            {
                result = TimePeriod.Pm;
                return true;
            }

            result = default;
            return false;
        }
    }
}