using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.DataStructures.Basics
{
    public class SimpleQueue<T>
    {
        private List<T> _items = new List<T>();

        public void Enqueue(T item)
        {
            _items.Add(item);
        }

        public T Dequeue()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("La cola está vacía");

            T item = _items[0];
            _items.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("La cola está vacía");

            return _items[0];
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsEmpty
        {
            get { return _items.Count == 0; }
        }
    }
}
