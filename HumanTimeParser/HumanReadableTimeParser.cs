using System;

namespace HumanTimeParser
{
    public static class HumanReadableTimeParser
    {
        /// <summary>Parses the time relative to DateTime.Now</summary>
        public static TimeParsingResult ParseTime(string time)
        {
            var parser = new Parser(time, null);

            return TimeParsingResult.FromNullableDateTime(parser.Parse(), parser.LastTokenPosition);
        }


    }
}
