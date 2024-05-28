using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgGame.Classes
{
    public class Speler
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int AttackChance { get; set; }
        public int Defense { get; set; }
        public int DefenseChance { get; set; }
        public int Heal { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Gold { get; set; }
        public string CurrentLocation { get; set; }
        public Inventory PlayerInventory { get; set; }
        public Equipment PlayerEquipment { get; set; }
        public Currency PlayerCurrency { get; set; }
        public PlayerClass PlayerClass { get; set; }
        private int experience;

        public int Experience
        {
            get => experience;
            set
            {
                experience = value;
                CheckLevelUp();
            }
        }

        public int Level { get; set; }

        public Speler()
        {
            Name = "";
            Class = "";
            Health = 100;
            Attack = 10;
            AttackChance = 10;
            Defense = 10;
            DefenseChance = 3;
            Heal = 20;
            MinDamage = 10;
            MaxDamage = 100;
            CurrentLocation = "Comfy City";
            PlayerInventory = new Inventory();
            PlayerEquipment = new Equipment();
            PlayerCurrency = new Currency();
            Gold = 0;
            Experience = 0;
            Level = 1;
        }

        public void ChoiceClass()
        {
            Console.WriteLine("\nChoose your Class!");
            Console.WriteLine("__________________");
            Console.WriteLine("1. Wizard");
            Console.WriteLine("2. Swordsman");
            Console.WriteLine("3. Elf");
            Console.WriteLine("__________________");

            if (int.TryParse(Console.ReadLine(), out var options))
            {
                switch (options)
                {
                    case 1:
                        PlayerClass = new Wizard();
                        Class = "Wizard";
                        break;
                    case 2:
                        PlayerClass = new Swordsman();
                        Class = "Swordsman";
                        break;
                    case 3:
                        PlayerClass = new Elf();
                        Class = "Elf";
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }

                PlayerClass?.SpecialAttack();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private void CheckLevelUp()
        {
            int[] experienceThresholds = { 100, 120, 160, 200 };
            int nextLevelThreshold = experienceThresholds.ElementAtOrDefault(Level - 1);

            if (nextLevelThreshold != 0 && Experience >= nextLevelThreshold)
            {
                Level++;
                Console.WriteLine($"Congratulations! You leveled up to level {Level}!");
            }
        }
    }

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
            Console.WriteLine($"Added {item.Name} to the inventory.");
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            Console.WriteLine($"Removed {item.Name} from the inventory.");
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

    public class Equipment
    {
        public Dictionary<EquipmentSlot, Item> EquippedItems { get; set; }

        public Equipment()
        {
            EquippedItems = new Dictionary<EquipmentSlot, Item>();
        }

        public void EquipItem(Item item, EquipmentSlot slot)
        {
            EquippedItems[slot] = item;
            Console.WriteLine($"Equipped {item.Name} in {slot} slot.");
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class Currency
    {
        public int Gold { get; set; }

        public Currency()
        {
            Gold = 0;
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"Added {amount} gold to the player's currency.");
        }

        public void SubtractGold(int amount)
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                Console.WriteLine($"Subtracted {amount} gold from the player's currency.");
            }
            else
            {
                Console.WriteLine("Not enough gold to perform the transaction.");
            }
        }
    }

    public class Shop
    {
        private List<Item> availableItems;

        public Shop()
        {
            availableItems = new List<Item>
            {
                new Item { Name = "Health Potion", Price = 10 },
                new Item { Name = "Sword", Price = 50 },
                new Item { Name = "Chest Armor", Price = 300 }
            };
        }

        public void DisplayItems()
        {
            Console.WriteLine("Items available in the shop:");
            foreach (var item in availableItems)
            {
                Console.WriteLine($"{item.Name} - Price: {item.Price} gold");
            }
        }

        public void BuyItem(Speler player, Item item)
        {
            var itemToBuy = availableItems.FirstOrDefault(i => i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
            if (itemToBuy != null)
            {
                if (player.PlayerCurrency.Gold >= itemToBuy.Price)
                {
                    player.PlayerInventory.AddItem(itemToBuy);
                    player.PlayerCurrency.SubtractGold(itemToBuy.Price);
                    Console.WriteLine($"Player bought {itemToBuy.Name} for {itemToBuy.Price} gold.");
                    
                    if (itemToBuy.Name == "Sword")
                    {
                        player.Attack += 20;
                    }
                }
                else
                {
                    Console.WriteLine("Not enough gold to buy the item.");
                }
            }
            else
            {
                Console.WriteLine("Item not found in the shop.");
            }
        }

        public void SellItem(Speler player, Item item)
        {
            if (player.PlayerInventory.Items.Contains(item))
            {
                player.PlayerInventory.RemoveItem(item);
                player.PlayerCurrency.AddGold(item.Price);
                Console.WriteLine($"Player sold {item.Name} for {item.Price} gold.");
            }
            else
            {
                Console.WriteLine("Player does not have the item to sell.");
            }
        }
    }

    public enum EquipmentSlot
    {
        Chest,
        Weapon,
        Leggings,
        Potion
    }

    public abstract class PlayerClass
    {
        public int Attack { get; set; }
        public string ClassName { get; set; }
        public int HealthUser { get; set; }
        public int Gold { get; set; }
        public string CurrentClass { get; set; }

        protected PlayerClass()
        {
            Attack = 10;
            ClassName = "";
            HealthUser = 100;
            Gold = 0;
            CurrentClass = "";
        }

        public abstract void SpecialAttack();

        public static PlayerClass ChooseClass()
        {
            Console.WriteLine("\nChoose your Class!");
            Console.WriteLine("__________________");
            Console.WriteLine("1. Wizard");
            Console.WriteLine("2. Swordsman");
            Console.WriteLine("3. Elf");
            Console.WriteLine("__________________");

            if (int.TryParse(Console.ReadLine(), out var options))
            {
                switch (options)
                {
                    case 1: return new Wizard();
                    case 2: return new Swordsman();
                    case 3: return new Elf();
                    default:
                        Console.WriteLine("Invalid Option");
                        return null;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return null;
            }
        }
    }
// Klasse die de 'Tovenaar' klasse representeert binnen het spel.
    public class Wizard : PlayerClass
    {
        // Constructor die de klasse naam instelt.
        public Wizard()
        {
            ClassName = "Wizard";
            HealthUser = 100;
            Gold = 0;
        }

        // Implementeert de speciale aanval voor de tovenaar.
        public override void SpecialAttack()
        {
            //decide the damage for the special attack until    function works probably 
            int specialAttackDamage = Attack = 25;
            Console.WriteLine("");
        }
    }

// Klasse die de 'Zwaardvechter' klasse representeert binnen het spel.
    public class Swordsman : PlayerClass
    {
        // Constructor die de klasse naam instelt.
        public Swordsman()
        {
            ClassName = "Swordsman";
        }

        // Implementeert de speciale aanval voor de zwaardvechter.
        public override void SpecialAttack()
        {
            int specialAttackDamage = Attack * 3;
            Console.WriteLine($"Performing special attack as a Swordsman. Damage: {specialAttackDamage}");
        }
    }

// Klasse die de 'Elf' klasse representeert binnen het spel.
    public class Elf : PlayerClass
    {
        // Constructor die de klasse naam instelt.
        public Elf()
        {
            ClassName = "Elf";
        }

        public int MagicDamage { get; set; }

        // Implementeert de speciale aanval voor de elf.
        public override void SpecialAttack()
        {
            int specialAttackDamage = MagicDamage * 4;
            Console.WriteLine($"Performing special attack as an Elf. Damage: {specialAttackDamage}");
        }
    }
}