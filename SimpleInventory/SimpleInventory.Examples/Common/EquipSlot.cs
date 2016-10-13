using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleInventory.Examples.Common
{
    /// <summary>
    /// Example of how to manage equipping items on a player
    /// An EquipSlot holds a single item, with optional constraints on
    /// the type it can hold. So you can't equip bags to your eyes (for example)
    /// Type constraints could also be done using attributes on the objects.
    /// 
    /// Also shown is maximum weight, so you can't carry a 500KG cheese wheel in your left hand.
    /// </summary>
    public class EquipSlot
    {
        /// <summary>
        /// The item equipped to this slot
        /// </summary>
        public Item Item { get; set; }

        public IEnumerable<Type> TypeConstraints { get; set; } 

        /// <summary>
        /// Maximum weight this equipment slot can hold
        /// </summary>
        public float MaxWeight { get; set; }

        public EquipSlot(float maxWeight, IEnumerable<Type> typeConstraints)
        {
            MaxWeight = maxWeight;
            TypeConstraints = typeConstraints;
        }

        public void Equip<T>(T item) where T:Item
        {
            if (TypeConstraints.Contains(typeof(T)))
                Item = item;
        }

        public EquipSlot() : this(float.NaN, new []{typeof(Item)}) {}

        public EquipSlot(IEnumerable<Type> typeConstraints) : this(float.NaN, typeConstraints) {}
    }

    // Example of constraint using an attribute:
    //
    // [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    // sealed class ClothingAttribute : Attribute { }
    // 
    // [Clothing]
    // public class Backpack : Item { }
    //
    // Make the EquipSlots Item member private:
    // public T Item {get; private set;}
    //
    // Make the TypeConstraints hold a list of Attribute
    // public IEnumerable<Attribute> TypeConstraints {get; set;}
    //
    // Then add an Equip method
    //
    // public void Equip(T item) {
    //  var itemAttributes = item.GetType().GetCustomAttributes(false);
    //
    //  if (itemAttributes.Any(attr => TypeConstraints.Contains(attr))) {
    //      Item = item;
    //  }
    // }
    // This method is slightly slower than type comparisons, but more flexible.
}