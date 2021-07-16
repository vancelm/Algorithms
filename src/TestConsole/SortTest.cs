using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

            Console.WriteLine("Unsorted:   " + string.Join(",", unsortedList));
            Console.WriteLine("List.Sort:  " + string.Join(",", sortedList));

            List<int> list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => BubbleSort(list), "BubbleSort: ");

            list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => QuickSort(list), "QuickSort1: ");

            list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => QuickSort2(list), "QuickSort2: ");

            list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => MergeSort(list), "MergeSort:  ");

            list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => HeapSort(list), "HeapSort1:  ");

            list = new List<int>(unsortedList);
            ValidateSort(sortedList, list, () => HeapSort2(list), "HeapSort2:  ");
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
            List<Action<List<int>>> sorts = new();
            sorts.Add(ListSort);
            sorts.Add(QuickSort);
            sorts.Add(QuickSort2);
            sorts.Add(MergeSort);
            sorts.Add(HeapSort);
            sorts.Add(HeapSort2);

            Console.WriteLine("Count,  List.Sort, QuickSort1, QuickSort2,  MergeSort,  HeapSort1,  HeapSort2");

            int iterations = 1000;
            for (int i = 100; i <= 10000; i += 100)
            {
                List<int> list = getList(i);
                Console.Write($"{i,5}");

                foreach (Action<List<int>> sort in sorts)
                {
                    double[] elapsed = new double[iterations];

                    Parallel.For(0, iterations, i =>
                    {
                        List<int> listCopy;
                        listCopy = new(list);
                        
                        elapsed[i] = TimeAlgorithm(() => sort(listCopy));
                    });

                    Console.Write($", {elapsed.Min(),10:0.0000}");
                }

                Console.WriteLine();
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
