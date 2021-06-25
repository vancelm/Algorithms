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
            Console.WriteLine("Test 1...");
            Test(100, 10000, 100, 1000, 1000, 1);
            Console.WriteLine();
            Console.WriteLine("Test 2...");
            Test(1000, 1000, 1, 100, 10000, 100);
        }

        private void Test(int countMin, int countMax, int countIncrement, int capacityMin, int capacityMax, int capacityIncrement)
        {
            Console.WriteLine("   Count, Capacity, Dynamic Time, Dynamic Max, Greedy Time, Greedy Max, Greedy % Optimal");
            for (int count = countMin; count <= countMax; count += countIncrement)
            {
                for (int capacity = capacityMin; capacity <= capacityMax; capacity += capacityIncrement)
                {
                    List<(int, int)> items = GetRandomItems(0, capacity, 0, capacity, count);
                    double dynamicDuration = double.MaxValue;
                    double dynamicValue = 0;
                    double greedyDuration = double.MaxValue;
                    double greedyValue = 0;

                    for (int k = 0; k < 10; k++)
                    {
                        dynamicDuration = Math.Min(dynamicDuration,
                            TestAlgorithm(() =>
                            {
                                dynamicValue = GetMaxValue_Dynamic(capacity, items);
                            }));
                        greedyDuration = Math.Min(greedyDuration,
                            TestAlgorithm(() =>
                            {
                                greedyValue = GetMaxValue_Greedy(capacity, items);
                            }));
                    }
                    double valueDifference = greedyValue / dynamicValue;

                    Console.Write($"{count,8},{capacity, 9},");
                    Console.Write($"{dynamicDuration, 13:0.0000}," +
                        $"{dynamicValue, 12}," +
                        $"{greedyDuration, 12:0.0000}," +
                        $"{greedyValue, 11},");
                    if (valueDifference == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine($"{valueDifference * 100, 16:###.00}%");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        private static List<(int Value, int Weight)> GetRandomItems(int minValue, int maxValue, int minWeight, int maxWeight, int count)
        {
            List<(int, int)> items = new(count);
            for (int i = 0; i < count; i++)
            {
                items.Add((random.Next(minValue, maxValue), random.Next(minWeight, maxWeight)));
            }
            return items;
        }
    }
}
