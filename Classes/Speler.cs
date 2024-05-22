using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgGame.Classes
{
    public class Speler
    {
        // Eigenschappen die basisattributen van de speler vastleggen.
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
        private int experience;

        // Property voor ervaring die automatisch het niveau van de speler controleert bij verandering.
        public int Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                CheckLevelUp();
            }
        }

        public int Level { get; set; }

        public Speler()
        {
            // Constructor die standaardwaarden instelt.
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

        // Methode die de speler toestaat om een klasse te kiezen bij het starten van het spel.
        public void ChoiceClass()
        {
            Console.WriteLine("\nChoose your Class! ");
            Console.WriteLine("__________________");
            Console.WriteLine("     1. Wizard");
            Console.WriteLine("     2. Swordsman");
            Console.WriteLine("     3. Elf");
            Console.WriteLine("__________________");

            var Options = int.Parse(Console.ReadLine());

            switch (Options)
            {
                case 1:
                    var wizard = new Wizard();// optie 1 tovenaar keuze
                    Class = "Wizard";
                    wizard.SpecialAttack();
                    break;
                case 2:
                    var swordsman = new Swordsman();// optie 2 ZwaardExpert keuze
                    Class = "Swordsman";
                    swordsman.SpecialAttack();
                    break;
                case 3:
                    var elf = new Elf();// optie 3 Elf keuze
                    Class = "Elf";
                    elf.SpecialAttack();
                    break;
                default:
                    Console.WriteLine("Invalid Option"); //foute invoer
                    break;
            }
        }

        // Private methode om te controleren of de speler een niveau omhoog is gegaan.
        private void CheckLevelUp()
        {
            //array voor willekeurige level verandering.
            int[] experienceThresholds = { 100, 120, 160, 200 };
            int nextLevelThreshold = experienceThresholds.ElementAtOrDefault(Level - 1);

            if (nextLevelThreshold != 0 && Experience >= nextLevelThreshold)
            {
                //als speler genoeg XP heeft dan gaat hij een level omhoog
                Level++;
                Console.WriteLine($"Congratulations! You leveled up to level {Level}!");
            }
        }
    }
    
    public class Inventory
    {
        // Eigenschappen die basisattributen van de inventaris vastleggen.
        public List<Item> Items { get; set; }

        public Inventory()
        {
            //lijst aanmaken voor item die er in komen zoals potion die je levenspunten toevoegt
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            //methode om de item toe te voegen aan je invetaris.
            Items.Add(item);
            Console.WriteLine($"Added {item.Name} to the inventory.");
        }

        public void RemoveItem(Item item)
        {
            //methode voor het verwijderen van je items 
            Items.Remove(item);
            Console.WriteLine($"Removed {item.Name} from the inventory.");
        }

        public void ShowInventoryUI()
        {
            //functie om je inventaris te zien en items te bekijken tot je beschikking
            Console.WriteLine("=========================");
            //controleert elk item die er in zal komen
            foreach (var item in Items)
            {
                Console.WriteLine($"| {item.Name}: {Items.Count(i => i.Name == item.Name)}");
            }

            Console.WriteLine("=========================");
        }
    }

    public class Equipment
    {
        //methode voor equipments die je aan kan zetten als character voor defense
        public Dictionary<EquipmentSlot, Item> EquippedItems { get; set; }
        //Dictionary toevoegen om de data die er bij de ID hoort te controleren of je correcte equipment heb.
        public Equipment()
        {
            //voeg de equipments in je inventaris 
            EquippedItems = new Dictionary<EquipmentSlot, Item>();
        }

        //methode neem de gegeven item toe aan een specifieke slot voor je speler
        public void EquipItem(Item item, EquipmentSlot slot)
        {
            EquippedItems[slot] = item;
            Console.WriteLine($"Equipped {item.Name} in {slot} slot.");
        }
    }

    public class Item
    {
        //autoproperties voor Item eigenschappen
        public string Name { get; set; }
        public int Price { get; set; } // De prijs
    }

    public class Currency
    {
        public int Gold { get; set; }// we betalen met goud oer en ouderwets

        //constructor voor eigenschap currency
        public Currency()
        {
            Gold = 0; //auto null
        }

        //amount meegeven in parameter 
        public void AddGold(int amount)
        {
            Gold += amount;
            Random rand = new Random();
            amount = rand.Next(1, 3);
            Console.WriteLine($"Added {amount} gold to the player's currency.");
        }

        //goud eraf halen en verminderen om de goud eraf te halen zodra de speler iets heeft gekocht
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
        //lijst voor mogelijke items 
        private List<Item> availableItems;

        public Shop()
        {
            // initaliseer de lijst voor items 
            availableItems = new List<Item>
            {
                //items toevoegen in de lijst met aangegeven prijzen toegeven
                new Item { Name = "Health Potion", Price = 10 },
                new Item { Name = "Sword", Price = 50 },
                new Item { Name = "Chest Armor", Price = 300 },
            };
        }

        //functie om aan te tonen welk items er zijn te koop
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
            // Zoek het eerste item in de lijst dat overeenkomt met de naam van het opgegeven item, ongeacht hoofdletters/kleine letters.
            var itemToBuy = availableItems.FirstOrDefault(i => i.Name.ToLower() == item.Name.ToLower());
            if (itemToBuy != null)
            {
                // Controleer of de speler voldoende goud heeft om het item te kopen.
                if (player.PlayerCurrency.Gold >= itemToBuy.Price)
                {
                    // Als het item een zwaard is, verhoog dan de aanvalskracht van de speler.
                    if (itemToBuy.Name == "Sword") // Check if the item is a sword
                    {
                        player.Attack += 20; // Verhoog de aanvalskracht van de speler.
                    }

                    // Voeg het gekochte item toe aan de inventaris van de speler.
                    player.PlayerInventory.AddItem(itemToBuy);
                    // Trek de prijs van het item af van het goud van de speler.
                    player.PlayerCurrency.SubtractGold(itemToBuy.Price);
                    Console.WriteLine($"Player bought {itemToBuy.Name} for {itemToBuy.Price} gold.");
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
            // Controleer of de speler het item bezit voordat het verkocht wordt.
            if (player.PlayerInventory.Items.Contains(item))
            {
                // Verwijder het item uit de inventaris van de speler.
                player.PlayerInventory.Items.Remove(item);
                // Voeg de waarde van het item toe aan het goud van de speler.
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
        //sloten die aanwezig zijn bij inventaris of moet je kopen
        Chest,
        Weapon,
        Leggings,
        Potion,
    }
}

// Abstracte basisklasse voor verschillende spelersklassen in het spel.
public abstract class PlayerClass
{
    // Basisattributen die voor alle klassen gelden.
    public int Attack;
    public string ClassName { get; set; }
    public int HealthUser { get; set; }
    public int Gold = 0;
    public string CurrentClass { get; set; }

    // Constructor die standaardwaarden instelt voor nieuwe spelersklassen.
    public PlayerClass()
    {
        Attack = 10;
        ClassName = ",";
        HealthUser = 100;
        Gold = Gold;
        CurrentClass = "";
    }

    // Abstracte methode die elke klasse moet implementeren voor hun speciale aanval.
    public abstract void SpecialAttack();

    // Methode die de speler toestaat om een klasse te kiezen.
    public void ChoiceClass()
    {
        Console.WriteLine("\nChoose your Class! ");
        Console.WriteLine("__________________");
        Console.WriteLine("     1. Wizard");
        Console.WriteLine("     2. Swordsman");
        Console.WriteLine("     3. Elf");
        Console.WriteLine("__________________");

        var Options = int.Parse(Console.ReadLine());

        switch (Options)
        {
            case 1:
                var wizard = new Wizard();
                CurrentClass = "Wizard";
                wizard.SpecialAttack();
                break;
            case 2:
                var swordsman = new Swordsman();
                CurrentClass = "Swordsman";
                swordsman.SpecialAttack();
                break;
            case 3:
                var elf = new Elf();
                CurrentClass = "Elf";
                elf.SpecialAttack();
                break;
            default:
                Console.WriteLine("Invalid Option");
                break;
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
    }

    // Implementeert de speciale aanval voor de tovenaar.
    public override void SpecialAttack()
    {
        int specialAttackDamage = Attack * 2;
        Console.WriteLine($"Performing special attack as a Wizard. Damage: {specialAttackDamage}");
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
