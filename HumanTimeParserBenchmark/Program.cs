using System;
using BenchmarkDotNet.Running;

namespace HumanTimeParserBenchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ParsingSpeedTest>();
        }
    }
}
