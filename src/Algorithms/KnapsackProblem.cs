using Algorithms.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public static class KnapsackProblem
    {
        public static int GetMaxValue_BruteForce(int capacity, List<KnapsackItem> items)
        {
            return GetMaxValue_Recursive(capacity, items, items.Count);
        }

        static int GetMaxValue_Recursive(int capacity, List<KnapsackItem> items, int n)
        {
            if (n == 0 || capacity == 0)
            {
                return 0;
            }

            if (items[n - 1].Weight > capacity)
            {
                return GetMaxValue_Recursive(capacity, items, n - 1);
            }
            else
            {
                return Math.Max(items[n - 1].Value + GetMaxValue_Recursive(capacity - items[n - 1].Weight, items, n - 1),
                    GetMaxValue_Recursive(capacity, items, n - 1));
            }
        }

        public static int GetMaxValue_Dynamic(int capacity, List<KnapsackItem> items)
        {
            int i, w;
            int[][] K = new int[items.Count + 1][];
            for (i = 0; i < K.Length; i++)
            {
                K[i] = new int[capacity + 1];
            }

            for (i = 0; i <= items.Count; i++)
            {
                for (w = 0; w <= capacity; w++)
                {
                    if (i == 0 || w == 0)
                    {
                        K[i][w] = 0;
                    }
                    else if (items[i - 1].Weight <= w)
                    {
                        K[i][w] = Math.Max(items[i - 1].Value + K[i - 1][w - items[i - 1].Weight],
                            K[i - 1][w]);
                    }
                    else
                    {
                        K[i][w] = K[i - 1][w];
                    }
                }
            }

            return K[items.Count][capacity];
        }

        public static int GetMaxValue_Greedy(int capacity, List<KnapsackItem> items)
        {
            int value = 0;
            int weight = 0;

            foreach(var item in items.OrderByDescending(i => i.ValuePerWeight))
            {
                if (weight + item.Weight < capacity)
                {
                    value += item.Value;
                    weight += item.Weight;
                }
            }

            return value;
        }
    }
}
