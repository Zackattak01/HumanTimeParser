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
        public static bool EndsWithAmPmSpecifier(this ReadOnlySpan<char> input)
        {
            if (!(input.Length >= 2))
                return false;

            var abbreviation = input.Slice(input.Length - 2);

            return abbreviation.Equals(Constants.Am, StringComparison.OrdinalIgnoreCase) || abbreviation.Equals(Constants.Pm, StringComparison.OrdinalIgnoreCase);
        }
    }
}