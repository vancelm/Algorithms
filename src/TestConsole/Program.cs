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

            Action test;
            Func<bool> validate;

            switch (args[0].Trim().ToLowerInvariant())
            {
                case "sort":
                    validate = () => SortTest.Validate();
                    test = () => SortTest.Test();
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

            AlgorithmTestHelper.Validate(validate);
            AlgorithmTestHelper.Test(test);
        }
    }
}
