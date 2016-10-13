using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleInventory.Examples.Common;

namespace SimpleInventory.Examples.InventoryLetters
{
    public class InventoryLetterBag<T> : Item, IContainer<T>
    {
        protected Inventory<T> Container = new Inventory<T>(maxSize: 26);

        private readonly Dictionary<T, string> _letterMap;
        private readonly BitArray _usedLetters;

        public InventoryLetterBag(string name, float volume, float weight) : base(name, volume, weight)
        {
            _letterMap = new Dictionary<T,string>(Container.MaximumSize);
            _usedLetters = new BitArray(Container.MaximumSize);
        }

        public bool Add(T item)
        {
            if (Container.Add(item))
            {
                // If the 
                if (!_letterMap.ContainsKey(item))
                {
                    var reservedLetter = false;

                    for (int i = 0; i < _usedLetters.Count; i++)
                    {
                        // The first time we hit an index that isn't set to on, use that.
                        if (!_usedLetters.Get(i))
                        {
                            _letterMap.Add(item, i.ToHexavigesimal());
                            _usedLetters.Set(i, true);
                            reservedLetter = true;
                        }
                    }

                    if (!reservedLetter)
                        throw new InventoryFullException("Inventory full or cannot assign letter to new item");
                }
                // TODO: Implement handling for stacking items under a single key.
                // This is left as an exercise to the reader.
            }

            return false;
        }

        public string TryGetLetterForItem(T item)
        {
            return _letterMap.ContainsKey(item) ? _letterMap[item] : null;
        }

        public void Remove(T item)
        {
            Container.Remove(item);

            if (_letterMap.ContainsKey(item))
            {
                // Unreserve the letter for this item.
                var index = _letterMap[item].FromHexavigesimal();
                _usedLetters.Set(index, false);
            }
        }

        public void Remove(Func<T, bool> predicate)
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

    public static class HexavigesimalExtensions
    {
        public static int FromHexavigesimal(this string s)
        {
            int i = 0;            
            s = new String(s.Reverse().ToArray());
            for (int p = s.Length - 1; p >= 0; p--)
            {
                char c = s[p];
                i += c.toInt() * (int)Math.Pow(26, p);
            }

            return i;
        }

        public static string ToHexavigesimal(this int i)
        {
            StringBuilder s = new StringBuilder();

            while (i > 26)
            {
                int r = i % 26;
                if (r == 0)
                {
                    i -= 26;
                    s.Insert(0, 'Z');
                }
                else
                {
                    s.Insert(0, r.toChar());
                }

                i = i / 26;
            }

            return s.Insert(0, i.toChar()).ToString();
        }

        private static char toChar(this int i)
        {
            return (char)(i + 64);
        }

        private static int toInt(this char c)
        {
            return (int)c - 64;
        }
    }
}