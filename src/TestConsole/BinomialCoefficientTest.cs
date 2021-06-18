using System;
using System.Collections.Generic;
using static Algorithms.BinomialCoefficient;

namespace TestConsole
{
    internal class BinomialCoefficientTest : AlgorithmTest
    {
        private static readonly IReadOnlyList<(int n, int k, int c)> expectedValues = new List<(int, int, int)>()
        {
            (0, 0, 1),
            (1, 0, 1),
            (1, 1, 1),
            (2, 0, 1),
            (2, 1, 2),
            (2, 2, 1),
            (3, 0, 1),
            (3, 1, 3),
            (3, 2, 3),
            (3, 3, 1),
            (4, 0, 1),
            (4, 1, 4),
            (4, 2, 6),
            (4, 3, 4),
            (4, 4, 1),
            (5, 0, 1),
            (5, 1, 5),
            (5, 2, 10),
            (5, 3, 10),
            (5, 4, 5),
            (5, 5, 1),
            (6, 0, 1),
            (6, 1, 6),
            (6, 2, 15),
            (6, 3, 20),
            (6, 4, 15),
            (6, 5, 6),
            (6, 6, 1),
            (7, 0, 1),
            (7, 1, 7),
            (7, 2, 21),
            (7, 3, 35),
            (7, 4, 35),
            (7, 5, 21),
            (7, 6, 7),
            (7, 7, 1),
            (8, 0, 1),
            (8, 1, 8),
            (8, 2, 28),
            (8, 3, 56),
            (8, 4, 70),
            (8, 5, 56),
            (8, 6, 28),
            (8, 7, 8),
            (8, 8, 1),
            (9, 0, 1),
            (9, 1, 9),
            (9, 2, 36),
            (9, 3, 84),
            (9, 4, 126),
            (9, 5, 126),
            (9, 6, 84),
            (9, 7, 36),
            (9, 8, 9),
            (9, 9, 1)
        };

        protected override bool ValidateAlgorithms()
        {
            bool isValid = true;

            if (!validateAlgorithm(BinomialCoefficient_DivideAndConquer, "Divide and Conquer"))
            {
                isValid = false;
            }

            if (!validateAlgorithm(BinomialCoefficient_Dynamic, "Dynamic"))
            {
                isValid = false;
            }

            if (!validateAlgorithm(BinomialCoefficient_Dynamic_Optimized, "Dynamic (Space Optimized)"))
            {
                isValid = false;
            }

            return isValid;
        }

        private static bool validateAlgorithm(Func<int, int, int> func, string name)
        {
            Console.WriteLine("Validating {0} algorithm...", name);
            foreach (var item in expectedValues)
            {
                int c = func(item.n, item.k);
                Console.WriteLine("{0}, {1}, {2}, {3}", item.n, item.k, item.c, c);
                if (c != item.Item3)
                {
                    Console.WriteLine("<FAIL>");
                    Console.WriteLine();
                    return false;
                }
            }

            Console.WriteLine("<PASS>");
            Console.WriteLine();
            return true;
        }

        protected override void TestAlgorithms()
        {
            Test(BinomialCoefficient_DivideAndConquer, "Divide and Conquer");
            Test(BinomialCoefficient_Dynamic, "Dynamic");
            Test(BinomialCoefficient_Dynamic_Optimized, "Dynamic (Space Optimized)");
        }

        private void Test(Func<int, int, int> func, string name)
        {
            Console.WriteLine();
            Console.WriteLine(name);

            for (int n = 0; n <= 30; n++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{n,3}");
                Console.ForegroundColor = ConsoleColor.White;

                double minDuration = double.MaxValue;

                for (int i = 0; i < 10; i++)
                {
                    double duration = TestAlgorithm(() =>
                    {
                        for (int k = 0; k <= n; k++)
                        {
                            func(n, k);
                        }
                    });

                    if (minDuration > duration)
                    {
                        minDuration = duration;
                    }

                    Console.Write($", {duration, 12:0.0000}");
                }

                Console.Write(", ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{minDuration, 12:0.0000}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            //Console.WriteLine($"Lowest Time: {lowestMs} ms");
        }
    }
}
