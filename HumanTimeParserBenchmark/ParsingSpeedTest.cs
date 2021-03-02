using System;
using BenchmarkDotNet.Attributes;
using HumanTimeParser;

namespace HumanTimeParserBenchmark
{
    public class ParsingSpeedTest
    {
        //toddler
        [Benchmark]
        public TimeParsingResult Parse0() => HumanReadableTimeParser.ParseTime("5s");
        //simple
        [Benchmark]
        public TimeParsingResult Parse1() => HumanReadableTimeParser.ParseTime("Wednesday on 5:55pm");
        //intermediate
        [Benchmark]
        public TimeParsingResult Parse2() => HumanReadableTimeParser.ParseTime("6 hours after 5:55pm on 1/21/2025");
        //stress test
        [Benchmark]
        public TimeParsingResult Parse3() => HumanReadableTimeParser.ParseTime("6 s 5 m 7 h 1 d 2 mth 3 y on 3/24/2021 at 4:56am");
    }
}