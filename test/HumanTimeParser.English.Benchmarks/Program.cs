using System;
using BenchmarkDotNet.Running;

namespace HumanTimeParser.English.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ParsingSpeedTest>();
        }
    }
}