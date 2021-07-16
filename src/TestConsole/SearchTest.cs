using System;
using System.Collections.Generic;
using static Algorithms.Search;

namespace TestConsole
{
    public static class SearchTest
    {
        private static readonly List<int> validationList = new() { 1, 4, 4, 6, 8, 9, 12, 15, 18, 23, 24 };
        private static readonly int validationValue = 15;
        private static readonly int validationIndex = 7;

        private static void Validate()
        {
            ValidateSearch(SequentialSearch, "Sequential");
        }

        private static void ValidateSearch(Func<int, IList<int>, int> search, string name)
        {
            int index = search(validationValue, validationList);

            Console.Write($"{name}: ");
            if (index == validationIndex)
                Console.Write("<PASS>");
            else
                Console.Write("<FAIL>");
        }

        public static void Test()
        {
            Validate();
        }
    }
}
