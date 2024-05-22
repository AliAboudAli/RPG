using System;
using System.Collections.Generic;

namespace RpgGame.Classes
{
    // Deze klasse beheert het verhaal en gebeurtenissen in het spel.
    public class Story
    {
        private readonly List<Enemy> enemies; // Een lijst om alle vijanden op te slaan.
        private readonly Random random; // Gebruikt voor willekeurige keuzes in het spel.

        private Speler player; // De speler die het spel speelt.
        private Enemy enemy; // De huidige vijand die de speler tegenkomt.

        // Start een nieuw verhaal en begint het avontuur van de speler.
        public Story(string nameFiller)
        {
            random = new Random();

            Console.WriteLine("Once upon a time, in a land filled with magic and mystery...");
            Console.WriteLine("You, a brave adventurer, set out on a journey to explore the unknown.");
            Console.WriteLine("Your path is filled with challenges and encounters. Let the adventure begin!");

            // Lijst van locaties die de speler kan verkennen.
            List<string> locations = new List<string>
                { "Scary Forest", "Mountain Dungeon", "Forgotten Temple", "Scorpion Valley" };
            enemies = InitializeEnemies(locations);

            player = new Speler();

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"\n=== Encounter {i} ===");
                Encounter();

                Console.WriteLine("What will you do?");
                Console.WriteLine("1. Engage in combat");
                Console.WriteLine("2. Choose a different path");

                int choice = GetValidInput(1, 2);

                if (choice == 1)
                {
                    Console.WriteLine($"Player {player.Name} is in combat!");
                    Combat();
                }
                else
                {
                    Console.WriteLine("You choose a different path, avoiding the encounter.");
                }
            }

            Console.WriteLine("\nCongratulations! You have completed 10 encounters. Your journey continues...");
            Console.WriteLine("To be continued...");
        }

        // Beheert de gevechten tijdens het spel.
        private void Combat()
        {
            while (true)
            {
                Console.WriteLine("=========================");
                Console.WriteLine("+ 1. Attack");
                Console.WriteLine("+ 2. Defense");
                Console.WriteLine("+ 3. Heal");

                if (player.Class.ToLower() == "wizard")
                {
                    Console.WriteLine("+ 4. Fireball");
                }
                else if (player.Class.ToLower() == "swordsman")
                {
                    Console.WriteLine("+ 5. Powerful Strike");
                }
                else if (player.Class.ToLower() == "elf")
                {
                    Console.WriteLine("+ 6. Magical Spell");
                }

                Console.WriteLine("+ 7. Run away");
                Console.WriteLine("=========================");

                Console.WriteLine("Please enter your choice:");
                var choice = GetValidInput(1, 7);

                switch (choice)
                {
                    case 1:
                        DoAttack(player);
                        break;
                    case 2:
                        DoDefense(player);
                        break;
                    case 3:
                        DoHeal(player);
                        break;
                    case 4:
                        if (player.Class.ToLower() == "wizard")
                        {
                            DoSpecialAttack(player);
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice, please try again.");
                        }
                        break;
                    case 5:
                        if (player.Class.ToLower() == "swordsman")
                        {
                            DoSpecialAttack(player);
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice, please try again.");
                        }
                        break;
                    case 6:
                        if (player.Class.ToLower() == "elf")
                        {
                            DoSpecialAttack(player);
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice, please try again.");
                        }
                        break;
                    case 7:
                        Console.WriteLine("You chose to run away!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                DoEnemyAttack();

                if (player.Health <= 0)
                {
                    Console.WriteLine("You have been defeated!");
                    return;
                }
                else if (enemy.Health <= 0)
                {
                    Console.WriteLine($"You defeated {enemy.Name}!");
                    return;
                }
            }
        }

        // Voert een aanval uit op de vijand.
        private void DoAttack(Speler player)
        {
            var attackSuccess = random.Next(0, 100) <= player.AttackChance;

            if (attackSuccess)
            {
                var damage = random.Next(player.MinDamage, player.MaxDamage + 1);
                enemy.Health -= damage;
                Console.WriteLine($"You dealt {damage} damage to {enemy.Name}!");
            }
            else
            {
                Console.WriteLine("Your attack missed!");
            }
        }

        // Speler probeert zich te verdedigen tegen een aanval.
        private void DoDefense(Speler player)
        {
            var defenseSuccess = player.Defense > 0 && player.DefenseChance > random.Next(0, 100);

            if (defenseSuccess)
            {
                Console.WriteLine("You successfully blocked the enemy's attack!");
            }
            else
            {
                var damage = random.Next(enemy.MinDamage, enemy.MaxDamage + 1);
                player.Health -= damage;
                Console.WriteLine($"Enemy dealt {damage} damage to you!");
            }
        }

        // Speler probeert te genezen tijdens het gevecht.
        private void DoHeal(Speler player)
        {
            if (player.Heal > 0)
            {
                var randNum = random.Next(1, 101);

                if (randNum <= 60)
                {
                    var healedAmount = random.Next(1, player.Heal + 1);
                    player.Health += healedAmount;
                    Console.WriteLine($"You have healed {healedAmount} HP!");
                }
                else if (randNum <= 90)
                {
                    var healedAmount = random.Next(1, player.Heal + 1);
                    player.Health += healedAmount;
                    Console.WriteLine($"You have healed {healedAmount} HP!");
                }
                else
                {
                    Console.WriteLine("You couldn't find an item to heal yourself!");
                }
            }
        }

        // Voert een speciale aanval uit afhankelijk van de klasse van de speler.
        private void DoSpecialAttack(Speler player)
        {
            switch (player.Class.ToLower())
            {
                case "wizard":
                    Console.WriteLine("Use your fireball!");
                    break;
                case "swordsman":
                    Console.WriteLine("Use your powerful sword strike!");
                    break;
                case "elf":
                    Console.WriteLine("Cast a magical spell as an elf!");
                    break;
                default:
                    Console.WriteLine("Invalid class for special attack.");
                    break;
            }
        }

        // De vijand valt de speler aan.
        private void DoEnemyAttack()
        {
            var attackSuccess = random.Next(0, 100) <= enemy.AttackChance;

            if (attackSuccess)
            {
                var damage = random.Next(enemy.MinDamage, enemy.MaxDamage + 1);
                player.Health -= damage;
                Console.WriteLine($"{enemy.Name} dealt {damage} damage to you!");
            }
            else
            {
                Console.WriteLine($"{enemy.Name}'s attack missed!");
            }
        }

        // Simuleert een ontmoeting met een vijand.
        private void Encounter()
        {
            int randomEnemyIndex = random.Next(enemies.Count);
            enemy = enemies[randomEnemyIndex];

            Console.WriteLine($"As you travel through the {enemy.Location}, you encounter {enemy.Name}!");
        }

        // Initialiseert de vijanden voor het spel.
        private List<Enemy> InitializeEnemies(List<string> locations)
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
            return enemies;
        }

        // Zorgt ervoor dat de gebruiker alleen geldige invoer geeft.
        private int GetValidInput(int minValue, int maxValue)
        {
            int choice;
            while (true)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= minValue && choice <= maxValue)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice.");
                }
            }

            return choice;
        }
    }
}
