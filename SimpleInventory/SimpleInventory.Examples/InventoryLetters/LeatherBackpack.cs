using SimpleInventory.Examples.Common;

namespace SimpleInventory.Examples.InventoryLetters
{
    public class LeatherBackpack : InventoryLetterBag<Item>
    {
        /// <inheritdoc />
        public LeatherBackpack() : base("Leather Backpack (Lettered)", Volumes.Litres(25), Weights.Kilograms(1))
        { }
    }
}