using System;
using System.Globalization;
using HumanTimeParser.Core.Culture;

namespace HumanTimeParser.Core.Tokenization
{
    public static class TokenizerUtils
    {
        public static bool TryParseNumber(string input, ITimeParsingCulture culture, out double number)
            => TryParseNumber(input.AsSpan(), culture, out number);

        public static bool TryParseNumber(ReadOnlySpan<char> input, ITimeParsingCulture culture, out double number)
            => double.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands,
                culture.CultureInfo, out number);

        public static bool TryParseDate(string input, ITimeParsingCulture culture, out DateTime date)
            => TryParseDate(input.AsSpan(), culture, out date);

        public static bool TryParseDate(ReadOnlySpan<char> input, ITimeParsingCulture culture, out DateTime date)
            => DateTime.TryParse(input, culture.CultureInfo, DateTimeStyles.None, out date);
        
        public static bool TryParseTimeSpan(string input, ITimeParsingCulture culture, out TimeSpan timeSpan)
            => TryParseTimeSpan(input.AsSpan(), culture, out timeSpan);

        public static bool TryParseTimeSpan(ReadOnlySpan<char> input, ITimeParsingCulture culture, out TimeSpan timeSpan)
            => TimeSpan.TryParse(input, culture.CultureInfo, out timeSpan);
    }
}