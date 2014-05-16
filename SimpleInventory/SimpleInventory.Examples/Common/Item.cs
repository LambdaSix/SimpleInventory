namespace SimpleInventory.Examples.Common
{
    public interface IVolumeItem
    {
        float Volume { get; }
        float Weight { get; }
    }

    /// <summary>
    /// Generic item class to represent a base for other items.
    /// </summary>
    public class Item : IVolumeItem
    {
        /// <summary>
        /// Name of the Item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Volume of the item in cubic metres
        /// </summary>
        public float Volume { get; set; }

        /// <summary>
        /// Weight of the item in grams
        /// </summary>
        public float Weight { get; set; }

        public Item(string name, float volume, float weight)
        {
            Name = name;
            Volume = volume;
            Weight = weight;
        }
    }
}