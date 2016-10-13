using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SimpleInventory.Examples.Common
{
    public class Player
    {
        /// <summary>
        /// Equipment slots available on the character
        /// </summary>
        public IDictionary<string, EquipSlot> EquipmentSlots { get; set; }

        public Player()
        {
            // A player has a number of equip slots, this allows for finer control
            // over where a character can put items, and things like:
            //  - Mutations rendering a equip slot unusable or improved (higher weight limit)
            //  - Characters with non-standard body shapes, 4 arms or multiple heads
            //  - Limb loss, no left leg to wear drop pouches on
            // Some of these things tie more into an anatomy system, but that's beyond this example.
            EquipmentSlots = new Dictionary<string, EquipSlot>
            {
                    // Grasp points and main container
                    { "Back", new EquipSlot(Weights.Kilograms(60), InventoryTypes.All) },
                    { "Left Hand", new EquipSlot(Weights.Kilograms(20), InventoryTypes.All) },
                    { "Right Hand", new EquipSlot(Weights.Kilograms(20), InventoryTypes.All) },

                    // Wearable points
                    // Obviously in a real system, these would be seperated more finely
                    // on type, putting pants on your feet or head isn't super sensible.
                    { "Head", new EquipSlot(Weights.Kilograms(10), InventoryTypes.Clothing) },
                    { "Torso", new EquipSlot(Weights.Kilograms(1), InventoryTypes.Clothing) },
                    { "Legs", new EquipSlot(Weights.Kilograms(20), InventoryTypes.Clothing) },
                    { "Left Leg", new EquipSlot(Weights.Kilograms(10), InventoryTypes.Clothing) },
                    { "Right Leg", new EquipSlot(Weights.Kilograms(10), InventoryTypes.Clothing) },
                    { "Feet", new EquipSlot(Weights.Kilograms(5), InventoryTypes.Clothing) },
            };
        }

        public EquipSlot Back { get { return EquipmentSlots["Back"]; } }
        public EquipSlot LeftHand { get { return EquipmentSlots["Left Hand"]; } }
        public EquipSlot RightHand { get { return EquipmentSlots["Right Hand"]; } }
        public EquipSlot Head { get { return EquipmentSlots["Head"]; } }
        public EquipSlot Torso { get { return EquipmentSlots["Torso"]; } }
        public EquipSlot Legs { get { return EquipmentSlots["Legs"]; } }
        public EquipSlot LeftLeg { get { return EquipmentSlots["Left Leg"]; } }
        public EquipSlot RightLeg { get { return EquipmentSlots["Right Leg"]; } }
        public EquipSlot Feet { get { return EquipmentSlots["Feet"]; } }
    }
}