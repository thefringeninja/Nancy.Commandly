using System;
using System.Linq;
using Simple.Testing.Framework;

namespace Nancy.Commandly.Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var results = SimpleRunner
                .RunFromGenerator(new Generator(typeof (Program).Assembly))
                .ToArray();
            var failed = results.Where(SpecificationFailed);
            if (failed.Any())
            {
                failed.ForEach(Print);
                Environment.ExitCode = failed.Count();
                return;
            }
            results.ForEach(Print);
        }

        private static void Print(RunResult runResult)
        {
            SpecificationPrinter.Print(runResult, Console.Out);
        }

        private static bool SpecificationFailed(RunResult result)
        {
            return false == result.Passed;
        }
    }
}