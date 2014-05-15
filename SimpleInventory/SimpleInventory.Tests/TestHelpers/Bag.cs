using System.Collections;
using System.Collections.Generic;

namespace SimpleInventory.Tests.TestHelpers
{
    public class Bag : Item, IContainer<Item>
    {
        private readonly Inventory<Item> _inventory = new Inventory<Item>(); 

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<Item> GetEnumerator()
        {
            return _inventory.GetEnumerator();
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

        public bool TryAdd(Item item)
        {
            return _inventory.TryAdd(item);
        }

        public void Add(Item item)
        {
            _inventory.Add(item);
        }

        public IEnumerable<IContainer<Item>> GetContainers()
        {
            return _inventory.GetContainers();
        }

        public IEnumerable<IContainer<U>> GetContainers<U>()
        {
            return _inventory.GetContainers<U>();
        }

        public IEnumerable<Item> Flatten()
        {
            return _inventory.Flatten();
        }
    }
}