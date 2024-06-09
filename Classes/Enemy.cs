using System;
using System.Collections.Generic;

namespace RpgGame.Classes
{
    // Deze klasse definieert de eigenschappen en gedragingen van een vijand in het spel.
    public class Enemy
    {
        // Constructor voor het aanmaken van een vijand met specifieke kenmerken.
        public Enemy(string name, int health, string behavior, string location)
        {
            Name = name;
            Health = health;
            Behaviour = behavior;
            Location = location;
            MinDamage = 1; // Minimum schade die deze vijand kan aanrichten.
            MaxDamage = 100; // Maximum schade die deze vijand kan aanrichten.
        }

        // Eigenschappen van de vijand.
        public string Name { get; set; }
        public int Health { get; set; }
        public string Behaviour { get; set; }
        public string Location { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int AttackChance { get; set; } // Kans op een succesvolle aanval van de vijand.

        // Een geneste klasse om karakters, zoals spelers, te beheren.
        public class Character
        {
            public string Name { get; set; }
            public int Health { get; set; }

            // Methode om schade te verwerken en de gezondheid van het karakter aan te passen.
            public void TakeDamage(int damage)
            {
                Health -= damage;

                if (Health <= 0)
                {
                    Console.WriteLine($"{Name} has died."); // Karakter is gestorven.
                }
            }
        }

        // Simuleert een actie van de vijand op zijn huidige locatie.
        public void PerformAction()
        {
            Character player = new Character
            {
                Name = "Player1",
                Health = 100
            };
            Console.WriteLine($"{Name} is doing something in {Location}.");
            Random random = new Random();
            int damage = random.Next(10, 21);
            player.TakeDamage(damage);
        }

        // Verplaatst de vijand willekeurig naar een nieuwe locatie, tenzij ze al in "Comfy City" zijn.
        public void MoveToRandomLocation(List<string> locations)
        {
            if (Location != "Comfy City")
            {
                Random random = new Random();
                int randomIndex = random.Next(locations.Count);
                Location = locations[randomIndex];
                Console.WriteLine($"{Name} moved to {Location}.");
            }
            else if (Location == "City of Awakening")
            {
                Random random = new Random();
                int randomIndex = random.Next(locations.Count);
                Location = locations[randomIndex];
                Console.WriteLine($"{Name} moved to {Location}.");
            }
            else
            {
                PerformAction(); // Voert een actie uit als de vijand niet verplaatst kan worden.
            }
        }

        // Initialiseert een lijst van vijanden op basis van gegeven locaties.
        public static List<Enemy> InitializeEnemies(List<string> locations)
        {
            var enemies = new List<Enemy>
            {
                new Enemy("Growling Hound", 100, "Growling Hound behavior", "Scary Forest"),
                new Enemy("Orcs", 100, "Orcs behavior", "Mountain Dungeon"),
                new Enemy("Werewolves", 100, "Werewolves behavior", "Scary Forest"),
                new Enemy("Corrupted Soldier", 100, "Corrupted Soldier behavior", "Mountain Dungeon"),
                new Enemy("Witch", 100, "Witch behavior", "Forgotten Temple"),
                new Enemy("Wizard", 100, "Wizard behavior", "Forgotten Temple"),
                new Enemy("Poisonous Scorpion", 100, "Poisonous Scorpion behavior", "Scorpion Valley")
            };
            return enemies; // Geeft de lijst van geïnitialiseerde vijanden terug.
        }
    }
}
