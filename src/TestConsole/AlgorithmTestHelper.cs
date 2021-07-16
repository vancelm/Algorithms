using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestConsole
{
    internal static class AlgorithmTestHelper
    {
        private static readonly Random random = new Random();

        public static double TimeAlgorithm(Action action)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static void ValidateAlgorithms(Func<bool> func)
        {
            Console.WriteLine("Validating...");
            bool isValid = func();

            if (isValid)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Validation passed");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Validation failed");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine();
        }
        public static List<int> GetRandomList(int count)
        {

            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(random.Next(count));
            return list;
        }

        public static List<int> GetSequentialList(int count)
        {
            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(i);
            return list;
        }

        public static List<int> GetReverseList(int count)
        {
            List<int> list = new List<int>(count);
            for (int i = count - 1; i >= 0; i--)
                list.Add(i);
            return list;
        }
    }
}
