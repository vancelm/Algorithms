using System;
using System.Diagnostics;

namespace TestConsole
{
    internal static class AlgorithmTestHelper
    {
        public static double TimeAlgorithm(Action action)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static void ValidateAlgorithms(Func<bool> func)
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
