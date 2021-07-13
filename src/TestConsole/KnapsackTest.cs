using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
            ValidateAlgorithm(GetMaxValue_BruteForce, "Brute Force");
            ValidateAlgorithm(GetMaxValue_Dynamic, "Dynamic");
            ValidateAlgorithm(GetMaxValue_Backtracking, "Backtracking");
            ValidateAlgorithm(GetMaxValue_Greedy, "Greedy");
            ValidateAlgorithm(GetMaxValue_BranchAndBound_BreadthFirst, "Branch-and-Bound (Breadth-First Search)");
            ValidateAlgorithm(GetMaxValue_BranchAndBound_BestFirst, "Branch-and-Bound (Best-First Search)");
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

        public static void Test()
        {
            Validate();

            Test(10, 400, 10, 100, 100, 1);
            Test(100, 100, 1, 10, 400, 10);
        }

        private static void Test_WriteHeading()
        {
            Console.WriteLine("Test 1...");
            
        }

        private static void Test(int countMin, int countMax, int countIncrement, int capacityMin, int capacityMax, int capacityIncrement, int valueMin = 0, int valueMax = 1000, int weightMin = 0, int weightMax = 1000, int iterations = 1)
        {
            List<Func<int, List<KnapsackItem>, int>> algorithms = new();
            algorithms.Add(GetMaxValue_Dynamic);
            algorithms.Add(GetMaxValue_Greedy);
            algorithms.Add(GetMaxValue_Backtracking);
            algorithms.Add(GetMaxValue_BranchAndBound_BreadthFirst);
            algorithms.Add(GetMaxValue_BranchAndBound_BestFirst);

            Console.Write("Count,Capacity,Value Range,Weight Range");

            foreach (var algorithm in algorithms)
            {
                Console.Write($",{algorithm.Method.Name}");
            }

            Console.WriteLine();

            for (int count = countMin; count <= countMax; count += countIncrement)
            {
                for (int capacity = capacityMin; capacity <= capacityMax; capacity += capacityIncrement)
                {
                    List<KnapsackItem> items = GetRandomItems(1, count / 2, 1, count / 2, count);
                    

                    Console.Write($"{count},{capacity},{valueMax - valueMin},{weightMax - weightMin}");

                    foreach (var algorithm in algorithms)
                    {
                        double duration = double.MaxValue;
                        int value = 0;

                        for (int i = 0; i < iterations; i++)
                        {
                            duration = Math.Min(duration, TimeAlgorithm(() =>
                            {
                                value = algorithm(capacity, items);
                            }));
                        }

                        Console.Write($",{duration}");
                    }

                    Console.WriteLine();
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
