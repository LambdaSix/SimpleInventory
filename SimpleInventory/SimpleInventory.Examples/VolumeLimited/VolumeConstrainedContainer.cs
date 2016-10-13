using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleInventory.Examples.Common;

namespace SimpleInventory.Examples.VolumeLimited
{
    public class VolumeConstrainedContainer<T> : Item, IContainer<T> where T:IVolumeItem
    {
        protected Inventory<T> Container = new Inventory<T>();
        
        public float MaxVolume { get; set; }
        public float MaxWeight { get; set; }

        protected float CurrentVolume { get; set; }
        protected float CurrentWeight { get; set; }

        public VolumeConstrainedContainer(string name, float volume, float weight, float maxVolume, float maxWeight)
                : base(name, volume, weight)
        {
            MaxVolume = maxVolume;
            MaxWeight = maxWeight;
        }

        public bool Add(T item)
        {
            // If adding this item would overflow our volume
            if (MaxVolume < (CurrentVolume + item.Volume))
            {
                return false;
            }

            // If adding this item would overflow our weight capacity
            if (MaxWeight < (CurrentWeight + item.Weight))
            {
                return false;
            }

            Container.Add(item);
            CurrentVolume += item.Volume;
            CurrentWeight += item.Weight;
            return true;
        }

        public void Remove(T item)
        {
            CurrentVolume -= item.Volume;
            CurrentWeight -= item.Weight;

            CurrentVolume = Math.Max(CurrentVolume, 0);
            CurrentWeight = Math.Max(CurrentWeight, 0);

            Container.Remove(item);
        }

        /// <summary>
        /// Remove all items from bag that match <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">Predicate matcher for removal</param>
        public void Remove(Func<T,bool> predicate)
        {
            foreach (var item in Container.Where(predicate))
            {
                Remove(item);
            }
        }

        public IEnumerable<IContainer<T>> GetContainers()
        {
            return Container.GetContainers();
        }

        public IEnumerable<IContainer<U>> GetContainers<U>()
        {
            return Container.GetContainers<U>();
        }

        public IEnumerable<T> Flatten()
        {
            return Container.Flatten();
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
            return Container.GetEnumerator();
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
            return Container.GetEnumerator();
        }
    }
}