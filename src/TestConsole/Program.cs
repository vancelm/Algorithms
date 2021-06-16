using System;

namespace TestConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args[0] != null)
            {
                switch (args[0].Trim().ToLowerInvariant())
                {
                    case "sorting":
                        RunSortingTests();
                        break;
                    case "binomial-coefficient":
                        RunBinomialCoefficientTests();
                        break;
                    default:
                        Console.WriteLine("Unknown test");
                        break;
                }
            }
        }

        private static void RunSortingTests()
        {

        }

        private static void RunBinomialCoefficientTests()
        {

        }
    }
}
