using System;
using System.Globalization;
using HumanTimeParser.Core.Culture;

namespace HumanTimeParser.Core.Tokenization
{
    /// <summary>
    /// Some utility methods for parsing that respect the <see cref="ITimeParsingCulture"/>
    /// </summary>
    public static class TokenizerUtils
    {
        /// <summary>
        /// Parses a number
        /// </summary>
        /// <param name="input">The input</param>
        /// <param name="culture">The culture</param>
        /// <param name="number">A number represented as a <see cref="double"/></param>
        /// <returns>A <see cref="bool"/> that indicates if the operation was successful</returns>
        public static bool TryParseNumber(string input, ITimeParsingCulture culture, out double number)
            => TryParseNumber(input.AsSpan(), culture, out number);

        /// <summary>
        /// Parses a number
        /// </summary>
        /// <param name="input">The input</param>
        /// <param name="culture">The culture</param>
        /// <param name="number">A number represented as a <see cref="double"/></param>
        /// <returns>A <see cref="bool"/> that indicates if the operation was successful</returns>
        public static bool TryParseNumber(ReadOnlySpan<char> input, ITimeParsingCulture culture, out double number)
            => double.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands,
                culture.CultureInfo, out number);

        /// <summary>
        /// Parses a date
        /// </summary>
        /// <param name="input">The input</param>
        /// <param name="culture">The culture</param>
        /// <param name="date">A date represented as a <see cref="DateTime"/></param>
        /// <returns>A <see cref="bool"/> that indicates if the operation was successful</returns>
        public static bool TryParseDate(string input, ITimeParsingCulture culture, out DateTime date)
            => TryParseDate(input.AsSpan(), culture, out date);

        /// <summary>
        /// Parses a date
        /// </summary>
        /// <param name="input">The input</param>
        /// <param name="culture">The culture</param>
        /// <param name="date">A date represented as a <see cref="DateTime"/></param>
        /// <returns>A <see cref="bool"/> that indicates if the operation was successful</returns>
        public static bool TryParseDate(ReadOnlySpan<char> input, ITimeParsingCulture culture, out DateTime date)
            => DateTime.TryParse(input, culture.CultureInfo, DateTimeStyles.None, out date);
        
        /// <summary>
        /// Parses a time span
        /// </summary>
        /// <param name="input">The input</param>
        /// <param name="culture">The culture</param>
        /// <param name="timeSpan">A <see cref="TimeSpan"/></param>
        /// <returns>A <see cref="bool"/> that indicates if the operation was successful</returns>
        public static bool TryParseTimeSpan(string input, ITimeParsingCulture culture, out TimeSpan timeSpan)
            => TryParseTimeSpan(input.AsSpan(), culture, out timeSpan);

        /// <summary>
        /// Parses a time span
        /// </summary>
        /// <param name="input">The input</param>
        /// <param name="culture">The culture</param>
        /// <param name="timeSpan">A <see cref="TimeSpan"/></param>
        /// <returns>A <see cref="bool"/> that indicates if the operation was successful</returns>
        public static bool TryParseTimeSpan(ReadOnlySpan<char> input, ITimeParsingCulture culture, out TimeSpan timeSpan)
            => TimeSpan.TryParse(input, culture.CultureInfo, out timeSpan);
    }
}