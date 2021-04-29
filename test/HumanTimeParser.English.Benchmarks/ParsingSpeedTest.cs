using BenchmarkDotNet.Attributes;
using HumanTimeParser.Core.Parsing;

namespace HumanTimeParser.English.Benchmarks
{
   
    [MemoryDiagnoser]
    public class ParsingSpeedTest
    {
        public static EnglishTimeParser parser = new EnglishTimeParser();
        //toddler
        [Benchmark] 
        public ITimeParsingResult Toddler() => parser.Parse("5s"); 
        //simple
        [Benchmark] 
        public ITimeParsingResult Simple() => parser.Parse("Wednesday on 5:55pm"); 
        //intermediate
        [Benchmark] 
        public ITimeParsingResult Intermediate() => parser.Parse("6 hours after 5:55pm on 1/21/2025"); 
        //stress test
        [Benchmark]
        public ITimeParsingResult StressTest() => parser.Parse("6 s 5 m 7 h 1 d 2 mth 3 y on 3/24/2021 at 4:56am");
        }
    
}