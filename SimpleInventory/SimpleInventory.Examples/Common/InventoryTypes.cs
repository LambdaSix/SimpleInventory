using System;
using System.Collections.Generic;

namespace SimpleInventory.Examples.Common
{
    /// <summary>
    /// Utility class to centralise type constraints for the examples.    
    /// </summary>
    public static class InventoryTypes
    {
        public static IEnumerable<Type> Clothing
        {
            get { return new[] { typeof (IClothing) }; }
        }

        public static IEnumerable<Type> All
        {
            get { return new[] { typeof (Item) }; }
        } 
    }
}