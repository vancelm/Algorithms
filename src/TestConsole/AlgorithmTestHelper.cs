using System;
using System.Diagnostics;

namespace TestConsole
{
    internal static class AlgorithmTestHelper
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();

        public static double TestAlgorithm(Action action)
        {
            stopwatch.Restart();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static void RunValidation(Func<bool> func)
        {
            Console.WriteLine("Validating...");
            bool isValid = func();

            if (isValid)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Validation passed");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Validation failed");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine();
        }
    }
}
