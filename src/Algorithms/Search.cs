using System;
using System.Collections.Generic;

namespace Algorithms
{
    public static class Search
    {
        public static int SequentialSearch<T>(T item, IList<T> list)
            where T : IEquatable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (item.Equals(list[i]))
                    return i;
            }

            return -1;
        }

        public static int BinarySearch<T>(T item, IList<T> list)
            where T : IComparable<T>
        {
            int min = 0;
            int max = list.Count - 1;

            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (item.CompareTo(list[mid]) == 0)
                    return mid;
                else if (item.CompareTo(list[mid]) < 0)
                    max = mid - 1;
                else
                    min = mid + 1;
            }

            return -1;
        }
    }
}
