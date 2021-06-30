using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Collections
{
    /// <summary>
    /// Represents a collection, with a fixed weight capacity, for storing items with value and weight.
    /// </summary>
    public class Knapsack : IList<KnapsackItem>
    {
        private readonly List<KnapsackItem> _list = new();

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
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than or equal to zero.");
            }

            Capacity = capacity;
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public KnapsackItem this[int index]
        {
            get => _list[index];
            set
            {
                if (value.Weight < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value.Weight), "Weight must be greater than or equal to zero.");
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

        /// <summary>
        /// Gets the number of elements contained in the <see cref="Knapsack"/>.
        /// </summary>
        public int Count => _list.Count;

        bool ICollection<KnapsackItem>.IsReadOnly => false;

        /// <summary>
        /// Adds an element to the end of the <see cref="Knapsack"/>.
        /// </summary>
        /// <param name="item">The element to add to the end of the <see cref="Knapsack"/></param>
        public void Add(KnapsackItem item)
        {
            if (item.Weight < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(item.Weight), "Weight must be greater than or equal to zero.");
            }

            if (Weight + item.Weight > Capacity)
            {
                throw new InvalidOperationException("Adding item would exceed weight capacity.");
            }

            _list.Add(item);
            Weight += item.Weight;
            Value += item.Value;
        }

        /// <summary>
        /// Removes all elements from the <see cref="Knapsack"/>
        /// </summary>
        public void Clear()
        {
            _list.Clear();
            Weight = 0;
            Value = 0;
        }

        public bool Contains(KnapsackItem item) => _list.Contains(item);

        public void CopyTo(KnapsackItem[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public IEnumerator<KnapsackItem> GetEnumerator() => _list.GetEnumerator();

        public int IndexOf(KnapsackItem item) => _list.IndexOf(item);

        public void Insert(int index, KnapsackItem item)
        {
            if (item.Weight < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(item.Weight), "Weight must be greater than or equal to zero.");
            }

            if (Weight + item.Weight > Capacity)
            {
                throw new InvalidOperationException("Inserting item would exceed weight capacity.");
            }

            _list.Insert(index, item);
            Weight += item.Weight;
            Value += item.Value;
        }

        public bool Remove(KnapsackItem item)
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
