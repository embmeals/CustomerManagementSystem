using System.Collections;

namespace CustomerManagementSystem.Data
{
    public class CustomerCollection<T> : IEnumerable<T>
    {
        private readonly List<T> _items;

        public CustomerCollection()
        {
            _items = new List<T>();
        }

        public int Count => _items.Count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range.");
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _items.Count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range.");
                _items[index] = value;
            }
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _items.Count)
                throw new IndexOutOfRangeException($"Index {index} is out of range.");
            _items.RemoveAt(index);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public List<T> GetAll()
        {
            return new List<T>(_items);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
