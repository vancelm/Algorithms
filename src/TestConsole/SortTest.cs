using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Algorithms.Sort;
using static TestConsole.AlgorithmTestHelper;

namespace TestConsole
{
    internal static class SortTest
    {
        private static readonly Random random = new Random();
        private static readonly Stopwatch stopwatch = new Stopwatch();

        private static void Validate()
        {
            List<int> unsortedList = GetRandomList(20);
            List<int> sortedList = new List<int>(unsortedList);
            sortedList.Sort(); // Assuming the built-in sort works ;)

            Console.WriteLine("Unsorted: " + string.Join(",", unsortedList));
            Console.WriteLine("Sorted:   " + string.Join(",", sortedList));

            List<int> list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => BubbleSort(list), "Bubble:   ");

            list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => QuickSort(list), "Quick:    ");

            list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => MergeSort(list), "Merge:    ");

            list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => HeapSort(list), "Heap:    ");
        }

        private static bool ValidateSort<T>(List<T> sortedList, List<T> list, Action sort, string name)
        {
            sort();
            Console.Write(name + string.Join(",", list));

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(sortedList[i]))
                {
                    Console.WriteLine(" <FAIL>");
                    return false;
                }
            }

            Console.WriteLine(" <PASS>");
            return true;
        }

        public static void Test()
        {
            Validate();

            Console.WriteLine();
            Console.WriteLine("Random");
            TestSorts(GetRandomList);

            Console.WriteLine();
            Console.WriteLine("Sequential");
            TestSorts(GetSequentialList);

            Console.WriteLine();
            Console.WriteLine("Reversed");
            TestSorts(GetReverseList);
        }

        private static void TestSorts(Func<int, List<int>> getList)
        {
            Console.WriteLine("Count, QuickSortRandom, QuickSortFixed, MergeSort, HeapSort, List.Sort");
            for (int i = 100; i <= 10000; i += 100)
            {
                List<int> list = getList(i);
                Console.Write(i + ",");
                //Console.Write(SortTest(() => new List<int>(list).BubbleSort()) + ",");
                Console.Write(TimeAlgorithm(() => QuickSort(new List<int>(list))) + ", ");
                Console.Write(TimeAlgorithm(() => QuickSort(new List<int>(list), false)) + ", ");
                Console.Write(TimeAlgorithm(() => MergeSort(new List<int>(list))) + ", ");
                Console.Write(TimeAlgorithm(() => HeapSort(new List<int>(list))) + ", ");
                Console.Write(TimeAlgorithm(() => new List<int>(list).Sort()) + "\r\n");
            }
        }

        private static List<int> GetRandomList(int count)
        {

            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(random.Next(count));
            return list;
        }

        private static List<int> GetSequentialList(int count)
        {
            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(i);
            return list;
        }

        private static List<int> GetReverseList(int count)
        {
            List<int> list = new List<int>(count);
            for (int i = count - 1; i >= 0; i--)
                list.Add(i);
            return list;
        }
    }
}
