using System;
using BenchmarkDotNet.Attributes;
using HumanTimeParser;

namespace HumanTimeParserBenchmark
{
    public class ParsingSpeedTest
    {
        [Benchmark]
        public TimeParsingResult Parse1() => HumanReadableTimeParser.ParseTime("Wednesday on 5:55pm");
        [Benchmark]
        public TimeParsingResult Parse2() => HumanReadableTimeParser.ParseTime("6 hours after 5:55pm on 1/21/2025");
        [Benchmark]
        public TimeParsingResult Parse3() => HumanReadableTimeParser.ParseTime("6 s 5 m 7 h 1 d 2 mth 3 y on 3/24/2021 at 4:56am");
    }
}