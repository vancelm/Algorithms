using System;

namespace TestConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No test specified.");
                return;
            }

            AlgorithmTest test;

            switch (args[0].Trim().ToLowerInvariant())
            {
                case "sort":
                    test = new SortTest();
                    break;
                case "binomial-coefficient":
                    test = new BinomialCoefficientTest();
                    break;
                case "knapsack":
                    test = new KnapsackTest();
                    break;
                default:
                    Console.WriteLine("Unknown test specified.");
                    return;
            }

            test.RunValidation();
            test.RunTest();
        }
    }
}
