using System;

namespace HumanTimeParser
{
    public static class HumanReadableTimeParser
    {
        /// <summary>Parses the time relative to DateTime.Now</summary>
        public static DateTime ParseTime(string time)
        {
            Parser parser = new Parser(time);
            return parser.Parse();
        }

    }
}
