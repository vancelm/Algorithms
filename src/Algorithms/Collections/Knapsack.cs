using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Collections
{
    /// <summary>
    /// Represents a collection, with a fixed weight capacity, for storing items with value and weight.
    /// </summary>
    public class Knapsack : IList<(int Value, int Weight)>
    {
        private List<(int Value, int Weight)> _list = new List<(int, int)>();

        /// <summary>
        /// Gets the maximum weight capacity of this knapsack.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// Gets the current weight of all items in this knapsack.
        /// </summary>
        public int Weight { get; private set; }

        /// <summary>
        /// Gets the current value of all items in this knapsack.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Knapsack"/> that is empty and has the specified weight capacity.
        /// </summary>
        /// <param name="capacity">The maximum weight allowed for all items combined.</param>
        public Knapsack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("Capacity must be greater than or equal to zero.");
            }

            Capacity = capacity;
        }

        public (int Value, int Weight) this[int index]
        {
            get => _list[index];
            set
            {
                if (value.Weight < 0)
                {
                    throw new ArgumentOutOfRangeException("Weight must be greater than or equal to zero.");
                }

                var oldItem = _list[index];
                if (Weight - oldItem.Weight + value.Weight > Capacity)
                {
                    throw new InvalidOperationException("Replacing item would exceed weight capacity.");
                }

                _list[index] = value;
                Weight = Weight - oldItem.Weight + value.Weight;
                Value = Value - oldItem.Value + value.Value;
            }
        }

        public int Count => _list.Count;

        bool ICollection<(int Value, int Weight)>.IsReadOnly => false;

        public void Add((int Value, int Weight) item)
        {
            if (item.Weight < 0)
            {
                throw new ArgumentOutOfRangeException("Weight must be greater than or equal to zero.");
            }

            if (Weight + item.Weight > Capacity)
            {
                throw new InvalidOperationException("Adding item would exceed weight capacity.");
            }

            _list.Add(item);
            Weight += item.Weight;
            Value += item.Value;
        }

        public void Clear()
        {
            _list.Clear();
            Weight = 0;
            Value = 0;
        }

        public bool Contains((int Value, int Weight) item) => _list.Contains(item);

        public void CopyTo((int Value, int Weight)[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public IEnumerator<(int Value, int Weight)> GetEnumerator() => _list.GetEnumerator();

        public int IndexOf((int Value, int Weight) item) => _list.IndexOf(item);

        public void Insert(int index, (int Value, int Weight) item)
        {
            if (item.Weight < 0)
            {
                throw new ArgumentOutOfRangeException("Weight must be greater than or equal to zero.");
            }

            if (Weight + item.Weight > Capacity)
            {
                throw new InvalidOperationException("Inserting item would exceed weight capacity.");
            }

            _list.Insert(index, item);
            Weight += item.Weight;
            Value += item.Value;
        }

        public bool Remove((int Value, int Weight) item)
        {
            bool isRemoved = _list.Remove(item);
            if (isRemoved)
            {
                Weight -= item.Weight;
            }

            return isRemoved;
        }

        public void RemoveAt(int index)
        {
            var item = _list[index];
            _list.RemoveAt(index);
            Weight -= item.Weight;
            Value -= item.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_list).GetEnumerator();
    }
}
