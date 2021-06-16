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
            Console.WriteLine("Validating...");
            bool isValid = ValidateAlgorithms();

            if (isValid)
            {
                Console.WriteLine("Validation passed");
            }
            else
            {
                Console.WriteLine("Validation failed");
            }
        }

        protected abstract bool ValidateAlgorithms();
    }
}
