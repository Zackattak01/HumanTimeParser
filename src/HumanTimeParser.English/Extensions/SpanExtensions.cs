using System;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.English.Extensions
{
    internal static class SpanExtensions
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
        
        public static ReadOnlySpan<char> GetNextNumber(this ReadOnlySpan<char> chars, int startPos = 0)
        {
            if (startPos > chars.Length)
                return ReadOnlySpan<char>.Empty;

            var lastDigitPos = startPos;

            while (lastDigitPos < chars.Length && (char.IsDigit(chars[lastDigitPos]) || char.IsPunctuation(chars[lastDigitPos])))
            {
                lastDigitPos++;
            }

            if (lastDigitPos == startPos)
                return ReadOnlySpan<char>.Empty;

            return chars.Slice(startPos, lastDigitPos - startPos);
        }

        public static ReadOnlySpan<char> GetNextWord(this ReadOnlySpan<char> chars, int startPos = 0)
        {
            var lastDigitPos = startPos;

            while (lastDigitPos < chars.Length && char.IsLetter(chars[lastDigitPos]))
            {
                lastDigitPos++;
            }

            if (lastDigitPos == startPos)
                return ReadOnlySpan<char>.Empty;

            return chars.Slice(startPos, lastDigitPos - startPos);
        }
    }
}