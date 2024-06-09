using System;
using System.Collections.Generic;

namespace RpgGame.Classes
{
    public enum EquipmentSlot
    {
        Head,
        Chest,
        Legs,
        Weapon,
        Shield
    }

    public class Item
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }
    }

    public class Equipment
    {
        public Dictionary<EquipmentSlot, Item> EquippedItems { get; set; }

        public Equipment()
        {
            EquippedItems = new Dictionary<EquipmentSlot, Item>();
        }

        public void EquipItem(Item item, EquipmentSlot slot)
        {
            if (EquippedItems.ContainsKey(slot))
            {
                Console.WriteLine($"Er zit al een item in het {slot} slot. Verwijder het eerst.");
                return;
            }

            EquippedItems[slot] = item;
            Console.WriteLine($"Uitgerust {item.Name} in {slot} slot.");
        }
    }
}