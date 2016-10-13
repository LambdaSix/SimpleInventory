using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SimpleInventory.Examples.Common;

namespace SimpleInventory.Examples.VolumeLimited
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void ContainerHasVolumeAndWeightLimit()
        {
            // Leatherpack holds 25litres and 60 kilograms
            var backpack = new LeatherBackpack();

            var smallHeavyItem = new Item("Unobtainium", Volumes.Litres(1), Weights.Kilograms(55));
            var stryofoam = new Item("Styrofoam", Volumes.Litres(24), Weights.Kilograms(5));
            var pieceOfStraw = new Item("Piece Of Straw", Volumes.Litres(1), Weights.Kilograms(1));

            Assert.That(backpack.Add(smallHeavyItem));
            Assert.That(backpack.Add(stryofoam));

            // This should fail.
            Assert.That(backpack.Add(pieceOfStraw) == false);

            var unobtainium = backpack.First(item => item.Name == "Unobtainium");
            backpack.Remove(unobtainium);

            Assert.That(backpack.Count() == 1);
        }
    }
}
