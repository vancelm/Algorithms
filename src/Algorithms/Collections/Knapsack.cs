﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Collections
{
    public class Knapsack : IList<(int Value, int Weight)>
    {
        private List<(int Value, int Weight)> _list = new List<(int, int)>();

        public int Capacity { get; }
        public int Weight { get; private set; }

        public Knapsack(int capacity)
        {
            Capacity = capacity;
        }

        public (int Value, int Weight) this[int index]
        {
            get => _list[index];
            set
            {
                var oldItem = _list[index];
                if (Weight - oldItem.Weight + value.Weight > Capacity)
                {
                    throw new InvalidOperationException("Replacing item would exceed weight capacity.");
                }

                _list[index] = value;
                Weight = Weight - oldItem.Weight + value.Weight;
            }
        }

        public int Count => _list.Count;

        bool ICollection<(int Value, int Weight)>.IsReadOnly => false;

        public void Add((int Value, int Weight) item)
        {
            if (Weight + item.Weight > Capacity)
            {
                throw new InvalidOperationException("Adding item would exceed weight capacity.");
            }

            _list.Add(item);
            Weight += item.Weight;
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains((int Value, int Weight) item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo((int Value, int Weight)[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<(int Value, int Weight)> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf((int Value, int Weight) item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, (int Value, int Weight) item)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove((int Value, int Weight) item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
