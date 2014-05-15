using System;
using System.Linq;
using NUnit.Framework;
using SimpleInventory.Tests.TestHelpers;

namespace SimpleInventory.Tests
{
    [TestFixture]
    public class InventoryTests
    {
        [Test]
        public void CanBeEnumerated()
        {
            var inv = new Inventory<float> { 4.0f, 8.0f, 16.0f };

            Assert.AreEqual((4.0f + 8.0f + 16.0f), inv.Sum());
        }

        [Test]
        public void CanBeSizeLimited()
        {
            var inv = new Inventory<float>(maxSize: 1) { 4.0f };

            Assert.Throws<InventoryFullException>(() => inv.Add(8.0f));

            Assert.AreEqual(1, inv.Count());
        }

        [Test]
        public void ContentsCanBeContainers()
        {
            var inv = new Inventory<Item>();
            var bagInv = new Bag { Name = "Leather Bag" };

            bagInv.Add(new Item { Name = "Rock" });

            inv.Add(new Item { Name = "Pencil" });
            inv.Add(new Item { Name = "Apple" });
            inv.Add(bagInv);

            var innerContainers = inv.GetContainers().ToList();

            Assert.AreEqual(1, innerContainers.Count());
            Assert.AreEqual("Rock", innerContainers.First().First().Name);
        }

        [Test]
        public void ContainersCanBeFlattened()
        {
            var inv = new Inventory<Item>();
            var bagInv = new Bag { Name = "Leather Bag" };
            var bag2Inv = new Bag { Name = "Cotton Bag" };

            bagInv.Add(new Item { Name = "Rock" });
            bag2Inv.Add(new Item { Name = "Paper" });

            inv.Add(new Item { Name = "Scissors" });
            inv.Add(bagInv);
            inv.Add(bag2Inv);

            var flat = inv.Flatten();
            Assert.AreEqual(5, flat.Count());
        }

        [Test]
        public void MultipleItems()
        {
            var inv = new Inventory<Item>
            {
                    new Item { Name = "Rock" },
                    new Item { Name = "Rock" },
                    new Dagger(), new Dagger()
            };

            Assert.AreEqual(2, inv.GetStackOf(item => item.Name == "Rock").Count());
            Assert.AreEqual(2, inv.GetStackOf<Dagger>(item => true).Count());

            var invInStacks = inv.GetStacks();
            Assert.AreEqual(2, invInStacks.Count());

            foreach (var group in invInStacks)
            {
                // The type of item
                Console.WriteLine(group.Key);
                // The items in question
                Console.WriteLine(String.Join(",", group.Select(item => item.Name)));
            }
        }
    }
}
