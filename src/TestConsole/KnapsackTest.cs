using static Algorithms.KnapsackProblem;
using System;
using System.Collections.Generic;

namespace TestConsole
{
    internal class KnapsackTest : AlgorithmTest
    {
        private static readonly Random random = new();
        private static readonly int validationValue = 217;
        private static readonly int validationCapacity = 100;
        private static readonly List<(int Value, int Weight)> validationList = new()
        {
            (43, 38),
            (49, 29),
            (57, 31),
            (60, 53),
            (67, 63),
            (68, 44),
            (72, 82),
            (84, 85),
            (87, 89),
            (92, 23),
        };

        protected override bool ValidateAlgorithms()
        {
            bool isValid = true;
            if (!ValidateAlgorithm(GetMaxValue_BruteForce, "Brute Force"))
            {
                isValid = false;
            }

            if (!ValidateAlgorithm(GetMaxValue_Dynamic, "Dynamic"))
            {
                isValid = false;
            }

            if (!ValidateAlgorithm(GetMaxValue_Greedy, "Greedy"))
            {
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateAlgorithm(Func<int, List<(int Value, int Weight)>, int> func, string name)
        {
            Console.WriteLine($"Validating {name} algorithm...");
            int value = func(validationCapacity, validationList);
            Console.WriteLine($"Expected Value: {validationValue}, Actual Value: {value}");
            if (value == validationValue)
            {
                Console.WriteLine("<PASS>");
                Console.WriteLine();
                return true;
            }
            else
            {
                Console.WriteLine("<FAIL>");
                Console.WriteLine();
                return false;
            }
        }

        protected override void TestAlgorithms()
        {
            int count = 100000;
            List<(int Value, int Weight)> items = new(count);
            for (int i = 0; i < count; i++)
            {
                items.Add((random.Next(1, 100), random.Next(1, 100)));
            }

            int capacity = 1000;
            double value = 0;
            double duration;
            //duration = TestAlgorithm(() => { value = GetMaxValue_BruteForce(capacity, items); });
            //Console.WriteLine($"Duration: {duration} ms, Value: {value}");
            duration = TestAlgorithm(() => { value = GetMaxValue_Dynamic(capacity, items); });
            Console.WriteLine($"Duration: {duration} ms, Value: {value}");
            duration = TestAlgorithm(() => { value = GetMaxValue_Greedy(capacity, items); });
            Console.WriteLine($"Duration: {duration} ms, Value: {value}");
        }
    }
}
