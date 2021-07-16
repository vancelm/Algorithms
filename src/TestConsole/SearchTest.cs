using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Algorithms.Search;
using static TestConsole.AlgorithmTestHelper;

namespace TestConsole
{
    public static class SearchTest
    {
        private static readonly List<int> validationList = new() { 1, 4, 4, 6, 8, 9, 12, 15, 18, 23, 24 };
        private static readonly int validationValue = 15;
        private static readonly int validationIndex = 7;

        private static void Validate()
        {
            Console.WriteLine("Validating...");
            ValidateSearch(SequentialSearch, "Sequential");
            ValidateSearch(BinarySearch, "Binary");
        }

        private static void ValidateSearch(Func<int, IList<int>, int> search, string name)
        {
            int index = search(validationValue, validationList);

            Console.Write($"{name}: ");
            if (index == validationIndex)
                Console.WriteLine("<PASS>");
            else
                Console.WriteLine("<FAIL>");
        }

        public static void Test()
        {
            Validate();
            Test1();
            Test2();
        }

        private static void Test1()
        {
            List<Func<int, List<int>, int>> searches = new();
            searches.Add(SequentialSearch);
            searches.Add(BinarySearch);

            Console.WriteLine("Test 1 (evenly distributed search values)");
            Console.WriteLine(" Count, Sequential,     Binary");

            for (int i = 100; i <= 1000; i += 100)
            {
                Console.Write($"{i,6}");

                List<int> list = GetSequentialList(i);
                foreach (var search in searches)
                {
                    double elapsed = TimeAlgorithm(() =>
                    {
                        for (int j = 0; j < i; j += i / 100)
                        {
                            search(j, list);
                        }
                    });

                    Console.Write($", {elapsed,10:0.0000}");
                }

                Console.WriteLine();
            }
        }

        private static void Test2()
        {
            List<Func<int, List<int>, int>> searches = new();
            searches.Add(SequentialSearch);
            searches.Add(BinarySearch);

            Console.WriteLine("Test 2 (worst case)");
            Console.WriteLine("    Count, Sequential,     Binary");

            for (int i = 1000; i <= 10000; i += 1000)
            {
                Console.Write($"{i,9}");

                List<int> list = GetSequentialList(i);
                foreach (var search in searches)
                {
                    double elapsed = TimeAlgorithm(() =>
                    {
                        search(i, list);
                    });

                    Console.Write($", {elapsed,10:0.0000}");
                }

                Console.WriteLine();
            }
        }
    }
}
