using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal abstract class AlgorithmTest
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        public void Test()
        {
            Console.WriteLine("Beginning test...");
            RunTests();
        }

        protected abstract void RunTests();

        protected double TestAlgorithm(Action action)
        {
            stopwatch.Restart();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public void Validate()
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
