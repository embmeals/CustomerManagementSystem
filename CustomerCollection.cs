using System.Collections;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.Data
{
    public class CustomerCollection<T> : IEnumerable<T> where T : IManageable
    {
        private readonly List<T> items = new();

        public CustomerCollection()
        {
        }

        public int Count => items.Count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= items.Count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range.");
                return items[index];
            }
            set
            {
                if (index < 0 || index >= items.Count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range.");
                items[index] = value;
            }
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public bool Remove(T item)
        {
            return items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= items.Count)
                throw new IndexOutOfRangeException($"Index {index} is out of range.");
            items.RemoveAt(index);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public List<T> GetAll()
        {
            return new List<T>(items);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
