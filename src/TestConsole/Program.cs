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

            switch (args[0].Trim().ToLowerInvariant())
            {
                case "sort":
                    SortTest.Test();
                    break;
                case "binomial-coefficient":
                    BinomialCoefficientTest.Test();
                    break;
                case "knapsack":
                    KnapsackTest.Test();
                    break;
                case "search":
                    SearchTest.Test();
                    break;
                default:
                    Console.WriteLine("Unknown test specified.");
                    return;
            }
        }
    }
}
