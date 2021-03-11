using System;
using System.Globalization;

namespace HumanTimeParser
{
    public static class SpanExtensions
    {
        public static int FirstNonNumberPos(this ReadOnlySpan<char> input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i]) && !char.IsPunctuation(input[i]))
                    return i;
            }

            return -1;
        }

        public static bool ContainsNumber(this ReadOnlySpan<char> input)
        {
            foreach (var c in input)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }

        public static bool EndsWithAmPmSpecifier(this ReadOnlySpan<char> input)
        {
            Span<char> lowerCaseSpan = new char[input.Length].AsSpan();
            input.ToLower(lowerCaseSpan, CultureInfo.CurrentCulture);
            //TODO: performace impact of hardcoding the specifiers as strings?
            if (lowerCaseSpan.EndsWith("am".AsSpan()) || lowerCaseSpan.EndsWith("pm".AsSpan()))
                return true;
            else
                return false;
        }
    }
}