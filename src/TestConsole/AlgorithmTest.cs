using System;
using System.Diagnostics;

namespace TestConsole
{
    internal abstract class AlgorithmTest
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

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
            Console.WriteLine("--- Begin Validation ---");
            bool isValid = ValidateAlgorithms();

            if (isValid)
            {
                Console.WriteLine("--- Validation Passed ---");
            }
            else
            {
                Console.WriteLine("--- Validation failed ---");
            }

            Console.WriteLine();
        }

        protected abstract bool ValidateAlgorithms();
    }
}
