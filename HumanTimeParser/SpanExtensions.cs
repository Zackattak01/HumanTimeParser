using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

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
            if (!(input.Length >= 2))
                return false;

            var abbreviation = input.Slice(input.Length - 2);
            //TODO: performace impact of hardcoding the specifiers as strings?
            if (abbreviation.Equals("am", StringComparison.OrdinalIgnoreCase) || abbreviation.Equals("pm", StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }
    }
}