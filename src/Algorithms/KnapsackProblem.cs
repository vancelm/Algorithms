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
            return BruteForce_Recursive(capacity, items, items.Count);
        }

        private static int BruteForce_Recursive(int capacity, List<KnapsackItem> items, int n)
        {
            if (n == 0 || capacity == 0)
            {
                return 0;
            }

            if (items[n - 1].Weight > capacity)
            {
                return BruteForce_Recursive(capacity, items, n - 1);
            }
            else
            {
                return Math.Max(items[n - 1].Value + BruteForce_Recursive(capacity - items[n - 1].Weight, items, n - 1),
                    BruteForce_Recursive(capacity, items, n - 1));
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

        public static int GetMaxValue_Backtracking(int capacity, List<KnapsackItem> items)
        {
            List<KnapsackItem> sortedItems = items.OrderByDescending(i => i.ValuePerWeight).ToList();
            bool[] include = new bool[items.Count];
            int maxProfit = 0;
            Backtracking_Recursive(capacity, sortedItems, -1, 0, 0, ref maxProfit, include);

            return maxProfit;
        }

        private static void Backtracking_Recursive(int capacity, List<KnapsackItem> items, int i, int value, int weight, ref int maxValue, bool[] include)
        {
            if (weight <= capacity && value > maxValue)
            {
                maxValue = value;
            }

            if (Backtracking_Promising(capacity, items, i, value, weight, maxValue))
            {
                include[i + 1] = true;
                Backtracking_Recursive(capacity, items, i + 1, value + items[i + 1].Value, weight + items[i + 1].Weight, ref maxValue, include);
                include[i + 1] = false;
                Backtracking_Recursive(capacity, items, i + 1, value, weight, ref maxValue, include);
            }
        }

        private static bool Backtracking_Promising(int capacity, List<KnapsackItem> items, int i, int value, int weight, int maxValue)
        {
            int j;
            int totalWeight;
            double bound;

            if (weight >= capacity)
            {
                return false;
            }
            else
            {
                j = i + 1;
                bound = value;
                totalWeight = weight;
                while (j < items.Count && totalWeight + items[j].Weight <= capacity)
                {
                    totalWeight += items[j].Weight;
                    bound += items[j].Value;
                    j++;
                }

                if (j < items.Count)
                {
                    bound += (capacity - totalWeight) * items[j].ValuePerWeight;
                }

                return bound > maxValue;
            }
        }

        private struct Node
        {
            public int Level;
            public int Value;
            public int Weight;
            public double Bound;
        }

        public static int GetMaxValue_BranchAndBound_BreadthFirst(int capacity, List<KnapsackItem> items)
        {
            if (items.Count == 0)
                return 0;

            List<KnapsackItem> sortedItems = items.OrderByDescending(i => i.ValuePerWeight).ToList();
            Queue<Node> queue = new();
            Node u = new();
            Node v = new();
            v.Level = -1;
            queue.Enqueue(v);
            int maxValue = 0;

            while (queue.Count > 0)
            {
                v = queue.Dequeue();
                u.Level = v.Level + 1;
                u.Value = v.Value + sortedItems[u.Level].Value;
                u.Weight = v.Weight + sortedItems[u.Level].Weight;
                if (u.Weight <= capacity && u.Value > maxValue)
                    maxValue = u.Value;
                
                if (Bound(capacity, sortedItems, u) > maxValue)
                    queue.Enqueue(u);

                u.Value = v.Value;
                u.Weight = v.Weight;
                if (Bound(capacity, sortedItems, u) > maxValue)
                    queue.Enqueue(u);
            }

            return maxValue;
        }

        public static int GetMaxValue_BranchAndBound_BestFirst(int capacity, List<KnapsackItem> items)
        {
            if (items.Count == 0)
                return 0;

            List<KnapsackItem> sortedItems = items.OrderByDescending(i => i.ValuePerWeight).ToList();
            PriorityQueue<Node, double> queue = new();
            Node u = new();
            Node v = new();
            v.Level = -1;
            v.Bound = Bound(capacity, items, v);
            
            queue.Enqueue(v, v.Bound);

            int maxValue = 0;

            while (queue.Count > 0)
            {
                v = queue.Dequeue();

                if (v.Bound > maxValue)
                {
                    u.Level = v.Level + 1;
                    u.Value = v.Value + sortedItems[u.Level].Value;
                    u.Weight = v.Weight + sortedItems[u.Level].Weight;
                    if (u.Weight <= capacity && u.Value > maxValue)
                        maxValue = u.Value;
                    
                    u.Bound = Bound(capacity, sortedItems, u);
                    if (u.Bound > maxValue)
                        queue.Enqueue(u, u.Bound);
                    
                    u.Value = v.Value;
                    u.Weight = v.Weight;
                    u.Bound = Bound(capacity, sortedItems, u);
                    if (u.Bound > maxValue)
                        queue.Enqueue(u, u.Bound);
                }
            }

            return maxValue;
        }

        private static float Bound(int capacity, List<KnapsackItem> items, Node u)
        {
            if (u.Weight >= capacity)
                return 0;

            float result = u.Value;
            int totalWeight = u.Weight;

            int j = u.Level + 1;
            while (j < items.Count && totalWeight + items[j].Weight <= capacity)
            {
                totalWeight += items[j].Weight;
                result += items[j].Value;
                j++;
            }

            if (j < items.Count)
                result += (capacity - totalWeight) * items[j].Value / items[j].Weight;

            return result;
        }
    }
}
