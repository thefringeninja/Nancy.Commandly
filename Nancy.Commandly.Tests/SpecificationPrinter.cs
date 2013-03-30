using System;
using System.IO;
using Simple.Testing.Framework;

namespace Nancy.Commandly.Tests
{
    public static class SpecificationPrinter
    {
        private static string NicePrint(object target)
        {
            return target == null ? "???" : target.ToString();
        }

        public static void Print(RunResult result, TextWriter output)
        {
            var passed = result.Passed ? "Passed" : "Failed";
            output.WriteLine(result.Name.Replace('_', ' ') + " - " + passed);
            if (result.Thrown != null)
            {
                output.WriteLine();
                output.WriteLine("Specification threw an exception.");
                output.WriteLine(result.Thrown);
                output.WriteLine();
                return;
            }
            var @on = result.GetOnResult();
            if (@on != null)
            {
                output.WriteLine();
                output.WriteLine("On:");
                output.WriteLine(NicePrint(@on));
                output.WriteLine();
            }

            if (result.Result != null)
            {
                output.WriteLine();
                output.WriteLine("Results with:");
                if (result.Result is Exception)
                    output.WriteLine(result.Result.GetType() + "\n" + ((Exception) result.Result).Message);
                else
                    output.WriteLine(NicePrint(result.Result));
                output.WriteLine();
            }

            output.WriteLine("Expectations:");
            foreach (var expecation in result.Expectations)
            {
                if (expecation.Passed)
                    output.WriteLine("\t" + expecation.Text + " " + (expecation.Passed ? "Passed" : "Failed"));
                else
                    output.WriteLine(expecation.Exception.Message);
            }
            if (result.Thrown != null)
            {
                output.WriteLine("Specification failed: " + result.Message);
                output.WriteLine();
                output.WriteLine(result.Thrown);
            }
            output.WriteLine(new string('-', 80));
            output.WriteLine();

            output.Flush();
        }
    }
}