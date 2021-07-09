using System;
using System.Collections.Generic;
using Algorithms;
using static Algorithms.KnapsackProblem;
using static TestConsole.AlgorithmTestHelper;

namespace TestConsole
{
    internal static class KnapsackTest
    {
        private static readonly Random random = new();
        private static readonly int validationValue = 217;
        private static readonly int validationCapacity = 100;
        private static readonly List<KnapsackItem> validationList = new()
        {
            new(43, 38),
            new(49, 29),
            new(57, 31),
            new(60, 53),
            new(67, 63),
            new(68, 44),
            new(72, 82),
            new(84, 85),
            new(87, 89),
            new(92, 23),
        };

        private static void Validate()
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

            if (!ValidateAlgorithm(GetMaxValue_Backtracking, "Backtracking"))
            {
                isValid = false;
            }

            if (!ValidateAlgorithm(GetMaxValue_Greedy, "Greedy"))
            {
                isValid = false;
            }

            if (!ValidateAlgorithm(GetMaxValue_BranchAndBound_BFS, "Branch and Bound (Breadth-First Search)"))
            {
                isValid = false;
            }

            return isValid;
        }

        private static bool ValidateAlgorithm(Func<int, List<KnapsackItem>, int> func, string name)
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

        public static void Test1()
        {
            Validate();

            Console.WriteLine("Test 1...");
            Console.WriteLine("   Count, Capacity, Dynamic Time, Backtracking Time, Greedy Time, Greedy % Optimal");
            Test1(100, 10000, 100, 1000, 1000, 1);

            Console.WriteLine();
            Console.WriteLine("Test 2...");
            Console.WriteLine("   Count, Capacity, Dynamic Time, Backtracking Time, Greedy Time, Greedy % Optimal");
            Test1(1000, 1000, 1, 100, 10000, 100);

            Console.WriteLine();
            Console.WriteLine("Test 3...");
            Console.WriteLine("   Count, Capacity, Value Range, Weight Range, Dynamic Time, Backtracking Time, Greedy Time, Greedy % Optimal");
            int min = 0;
            int max = 1000;
            while (min < max)
            {
                Test1(1000, 1000, 1, 1000, 1000, 1, min, max, 0, 1000);
                min += 10;
                max -= 10;
            }

            Console.WriteLine();
            Console.WriteLine("Test 4...");
            Console.WriteLine("   Count, Capacity, Value Range, Weight Range, Dynamic Time, Backtracking Time, Greedy Time, Greedy % Optimal");
            min = 0;
            max = 1000;
            while (min < max)
            {
                Test1(1000, 1000, 1, 1000, 1000, 1, 0, 1000, min, max);
                min += 10;
                max -= 10;
            }

            Console.WriteLine();
            Console.WriteLine("Test 5...");
            Console.WriteLine("   Count, Capacity, Value Range, Weight Range, Dynamic Time, Backtracking Time, Greedy Time, Greedy % Optimal");
            min = 0;
            max = 1000;
            while (min < max)
            {
                Test1(1000, 1000, 1, 1000, 1000, 1, min, max, min, max);
                min += 10;
                max -= 10;
            }
        }

        private static void Test1(int countMin, int countMax, int countIncrement, int capacityMin, int capacityMax, int capacityIncrement, int valueMin = 0, int valueMax = 1000, int weightMin = 0, int weightMax = 1000)
        {
            for (int count = countMin; count <= countMax; count += countIncrement)
            {
                for (int capacity = capacityMin; capacity <= capacityMax; capacity += capacityIncrement)
                {
                    List<KnapsackItem> items = GetRandomItems(weightMin, weightMax, valueMin, valueMax, count);
                    double dynamicDuration = double.MaxValue;
                    double dynamicValue = 0;
                    double backtrackingDuration = double.MaxValue;
                    double backtrackingValue = 0;
                    double greedyDuration = double.MaxValue;
                    double greedyValue = 0;

                    for (int k = 0; k < 10; k++)
                    {
                        dynamicDuration = Math.Min(dynamicDuration,
                            TimeAlgorithm(() =>
                            {
                                dynamicValue = GetMaxValue_Dynamic(capacity, items);
                            }));
                        backtrackingDuration = Math.Min(backtrackingDuration,
                            TimeAlgorithm(() =>
                            {
                                backtrackingValue = GetMaxValue_Backtracking(capacity, items);
                            }));
                        greedyDuration = Math.Min(greedyDuration,
                            TimeAlgorithm(() =>
                            {
                                greedyValue = GetMaxValue_Greedy(capacity, items);
                            }));
                    }
                    double valueDifference = greedyValue / dynamicValue;

                    Console.Write($"{count,8},{capacity, 9},{valueMax - valueMin, 12},{weightMax - weightMin, 13},");
                    Console.Write($"{dynamicDuration, 13:0.0000}," +
                        $"{backtrackingDuration, 18:0.0000}," +
                        $"{greedyDuration, 12:0.0000},");
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

        private static List<KnapsackItem> GetRandomItems(int minValue, int maxValue, int minWeight, int maxWeight, int count)
        {
            List<KnapsackItem> items = new(count);
            for (int i = 0; i < count; i++)
            {
                items.Add(new(random.Next(minValue, maxValue), random.Next(minWeight, maxWeight)));
            }
            return items;
        }
    }
}
