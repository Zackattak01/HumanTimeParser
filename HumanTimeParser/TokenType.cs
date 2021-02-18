using System;

namespace HumanTimeParser
{
    [Flags]
    internal enum TokenType
    {
        None = 0,
        END = 1 << 0,
        Number = 1 << 1,
        Second = 1 << 2,
        Minute = 1 << 3,
        Hour = 1 << 4,
        Day = 1 << 5,
        Week = 1 << 6,
        Month = 1 << 7,
        Year = 1 << 8,
        TimeOfDay = 1 << 9,
        Date = 1 << 10,
        Tomorrow = 1 << 11,
        TwelveHourSpecifier = 1 << 12
    }
}