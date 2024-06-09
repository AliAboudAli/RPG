using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgGame.Classes
{
    public class Inventory
    {
        public List<Item> Items { get; set; }

        public Inventory()
        {
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
            Console.WriteLine($"Toegevoegd {item.Name} aan de inventaris.");
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            Console.WriteLine($"Verwijderd {item.Name} uit de inventaris.");
        }

        public void ShowInventoryUI()
        {
            Console.WriteLine("=========================");
            foreach (var item in Items.Distinct())
            {
                Console.WriteLine($"| {item.Name}: {Items.Count(i => i.Name == item.Name)}");
            }
            Console.WriteLine("=========================");
        }
    }
}