using System;

namespace HumanTimeParser
{
    internal enum TimeToken
    {
        END,
        Value,
        Second,
        Minute,
        Hour,
        Day,
        Week,
        Month,
        Year,
        TimeOfDay,
        Date,
    }
}