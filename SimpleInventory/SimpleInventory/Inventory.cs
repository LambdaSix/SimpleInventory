using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleInventory
{
    [Serializable]
    public class InventoryFullException : Exception
    {
        public InventoryFullException() {}
        public InventoryFullException(string message) : base(message) {}
        public InventoryFullException(string message, Exception inner) : base(message, inner) {}

        protected InventoryFullException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }

    public interface IContainer<T> : IEnumerable<T>
    {
        bool Add(T item);
        IEnumerable<IContainer<T>> GetContainers();
        IEnumerable<IContainer<U>> GetContainers<U>();
        IEnumerable<T> Flatten();
    }

    public class Inventory<T> : IContainer<T>
    {
        private readonly bool _fixedSize;
        private readonly ICollection<T> _collection;
        private int _maxSize;

        public int MaximumSize
        {
            get { return _maxSize; }
            set { _maxSize = value; }
        }

        public Inventory() : this(1, fixedSize: false) {}

        public Inventory(int maxSize) : this(maxSize, fixedSize: true) {}

        private Inventory(int initialSize, bool fixedSize)
        {
            _fixedSize = fixedSize;

            if (fixedSize)
            {
                _maxSize = initialSize;
            }

            _collection = new List<T>(initialSize);
        }

        public bool Add(T item)
        {
            if (!_fixedSize || _collection.Count < _maxSize)
            {
                _collection.Add(item);
                return true;
            }

            return false;
        }

        public ILookup<T, T> GetStacks()
        {
            return _collection.ToLookup(t => t);
        }

        public IEnumerable<T> GetStackOf(Func<T, bool> predicate)
        {
            return _collection.Where(predicate);
        }

        public IEnumerable<U> GetStackOf<U>()
        {
            return _collection.OfType<U>();
        }

        public IEnumerable<U> GetStackOf<U>(Func<U, bool> predicate)
        {
            return _collection.OfType<U>().Where(predicate);
        }

        public IEnumerable<T> Flatten()
        {
            foreach (var item in _collection)
            {
                yield return item;
                if (item is IContainer<T>)
                {
                    foreach (var subItem in (item as IContainer<T>).Flatten())
                    {
                        yield return subItem;
                    }
                }
            }
        }

        public IEnumerable<IContainer<U>> GetContainers<U>()
        {
            return _collection.OfType<IContainer<U>>();
        }

        public IEnumerable<IContainer<T>> GetContainers()
        {
            return _collection.OfType<IContainer<T>>();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}