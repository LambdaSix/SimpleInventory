using SimpleInventory.Examples.Common;

namespace SimpleInventory.Examples.VolumeLimited
{
    public class LeatherBackpack : VolumeConstrainedContainer<Item>
    {
        public LeatherBackpack() : base("Leather Backpack", Volumes.Litres(25), Weights.Kilograms(1), Volumes.Litres(25), Weights.Kilograms(60)) {}
    }
}