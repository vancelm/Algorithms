using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Algorithms.Sort;

namespace TestConsole
{
    internal class SortTest : AlgorithmTest
    {
        private readonly Random random = new Random();
        private readonly Stopwatch stopwatch = new Stopwatch();

        protected override bool ValidateAlgorithms()
        {
            bool isValid = true;

            List<int> unsortedList = GetRandomList(20);
            List<int> sortedList = new List<int>(unsortedList);
            sortedList.Sort(); // Assuming the built-in sort works ;)

            Console.WriteLine("Unsorted: " + string.Join(",", unsortedList));
            Console.WriteLine("Sorted:   " + string.Join(",", sortedList));

            List<int> list = new List<int>(unsortedList);
            if (!ValidateSort(sortedList, list, () => BubbleSort(list), "Bubble:   "))
            {
                isValid = false;
            }

            list = new List<int>(unsortedList);
            if (!ValidateSort(sortedList, list, () => QuickSort(list), "Quick:    "))
            {
                isValid = false;
            }

            list = new List<int>(unsortedList);
            if (!ValidateSort(sortedList, list, () => MergeSort(list), "Merge:    "))
            {
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateSort<T>(List<T> sortedList, List<T> list, Action sort, string name)
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

        protected override void TestAlgorithms()
        {
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

        private void TestSorts(Func<int, List<int>> getList)
        {
            for (int i = 1; i <= 1000000000; i *= 10)
            {
                List<int> list = getList(i);
                Console.Write(i + ",");
                //Console.Write(SortTest(() => new List<int>(list).BubbleSort()) + ",");
                Console.Write(TestAlgorithm(() => QuickSort(new List<int>(list))) + ",");
                Console.Write(TestAlgorithm(() => MergeSort(new List<int>(list))) + ",");
                Console.Write(TestAlgorithm(() => new List<int>(list).Sort()) + "\r\n");
            }
        }

        private List<int> GetRandomList(int count)
        {

            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(random.Next(count));
            return list;
        }

        private List<int> GetSequentialList(int count)
        {
            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(i);
            return list;
        }

        private List<int> GetReverseList(int count)
        {
            List<int> list = new List<int>(count);
            for (int i = count - 1; i >= 0; i--)
                list.Add(i);
            return list;
        }
    }
}
