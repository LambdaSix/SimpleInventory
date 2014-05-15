using System;

namespace SimpleInventory.Tests.TestHelpers
{
    public class Item : IEquatable<Item>
    {
        public string Name { get; set; }

        public bool Equals(Item other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            if (other.Name == Name)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}