using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Collections
{
    public class Knapsack : IList<(int Value, int Weight)>
    {
        private List<(int Value, int Weight)> _list = new List<(int, int)>();

        public (int Value, int Weight) this[int index] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int Count => throw new System.NotImplementedException();

        public bool IsReadOnly => throw new System.NotImplementedException();

        public void Add((int Value, int Weight) item)
        {
            throw new System.NotImplementedException();
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
