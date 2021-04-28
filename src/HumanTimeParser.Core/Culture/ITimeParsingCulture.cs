using System.Globalization;
using HumanTimeParser.Core.TimeConstructs;

namespace HumanTimeParser.Core.Culture
{
    public interface ITimeParsingCulture
    {
        public CultureInfo CultureInfo { get; }
        public ClockType ClockType { get; }
    }
}