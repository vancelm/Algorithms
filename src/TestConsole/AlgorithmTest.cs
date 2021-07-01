using System;
using System.Diagnostics;

namespace TestConsole
{
    internal abstract class AlgorithmTest
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();

        public void RunTest()
        {
            Console.WriteLine("Beginning test...");
            TestAlgorithms();
        }

        protected abstract void TestAlgorithms();

        protected double TestAlgorithm(Action action)
        {
            stopwatch.Restart();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public void RunValidation()
        {
            Console.WriteLine("Validating...");
            bool isValid = ValidateAlgorithms();

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

        protected abstract bool ValidateAlgorithms();
    }
}
