using System;
using System.Threading;

namespace RpgGame.Classes
{
    public class Game
    {
        private int seconds = 5;

        public void MenuStart()
        {
            Console.WriteLine("Welcome to World of RPG's");
            var options = new Options();
            Console.WriteLine("\nChoose your input:");
            Console.WriteLine("\n1. Start Game");
            Console.WriteLine("\n2. Options");
            Console.WriteLine("\n3. Exit Game:");

            var choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RunGame();
                    break;

                case 2:
                    options.OptionMenu();
                    break;

                case 3:
                    for (int i = 0; i < seconds; i++)
                    {
                        Console.Clear();
                        Console.Write($"Closing in.... {seconds - i} seconds");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Thread.Sleep(1050);
                        Console.Beep(700, 1000);
                    }
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Incorrect input, try again");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
            }
            Console.Clear();
        }

        public void RunGame()
        {
            Console.Clear();
            Speler player = new Speler();
            Console.WriteLine("Please enter your nickname:");
            player.Name = Console.ReadLine();
            Console.Clear();

            player.ChoiceClass();
            player.Level = 1;

            Item healthPotion = new Item { Name = "Health Potion", Price = 10 };
            player.PlayerEquipment.EquipItem(healthPotion, EquipmentSlot.Potion);

            Mapping map = new Mapping(player);
            Shop shop = new Shop();

            map.StartStory();
            map.CurrentLocation = "Comfy City";

            while (true)
            {
                Console.WriteLine($"You are currently in: {map.CurrentLocation}");
                Console.WriteLine("1. Travel to a new location");
                Console.WriteLine("2. Visit the shop");
                Console.WriteLine("3. Display player information");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        map.Travel();
                        map.Encounter();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Welcome to the shop!");
                        shop.DisplayItems();
                        shop.BuyItem(player, healthPotion);
                        shop.SellItem(player, healthPotion);
                        break;
                    case 3:
                        DisplayPlayerInfo(player);
                        break;
                }

                if (player.Health <= 0)
                {
                    Combat com = new Combat(
                        player.Name,
                        player.Class,
                        player.Health,
                        player.Attack,
                        player.AttackChance,
                        player.Defense,
                        player.DefenseChance,
                        player.Heal,
                        player.MinDamage,
                        player.MaxDamage
                    );
                    com.Die();
                    return;
                }

                if (choice != 3)
                {
                    Console.WriteLine("Combat Options:");
                    map.Combat.CombatOptions();
                    int combatChoice = Convert.ToInt32(Console.ReadLine());
                    switch (combatChoice)
                    {
                        case 1:
                            map.Combat.Attack(player, map.EncounteredEnemy);
                            break;
                        case 2:
                            map.Combat.Defend(player);
                            break;
                        case 3:
                            map.Combat.Heal(player);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                    if (map.EncounteredEnemy.Health <= 0)
                    {
                        map.Combat.Victory(player, map.EncounteredEnemy);
                    }
                    if (player.Health <= 0)
                    {
                        Combat com = new Combat(
                            player.Name,
                            player.Class,
                            player.Health,
                            player.Attack,
                            player.AttackChance,
                            player.Defense,
                            player.DefenseChance,
                            player.Heal,
                            player.MinDamage,
                            player.MaxDamage
                        );
                        com.Die();
                        return;
                    }
                }
            }
        }

        public void DisplayPlayerInfo()
        {
            Console.WriteLine($"Player Name: {player.Name}");
            Console.WriteLine($"Player Class: {player.Class}");
            Console.WriteLine($"Player Health: {player.Health}");
            Console.WriteLine($"Player Attack: {player.Attack}");
            Console.WriteLine($"Player Defense: {player.Defense}");
            Console.WriteLine($"Player Gold: {player.PlayerCurrency.Gold}");
            Console.WriteLine($"Player Level: {player.Level}");
            Console.WriteLine("Player Inventory:");
            player.PlayerInventory.ShowInventoryUI();
            Console.WriteLine("Player Equipment:");
            foreach (var kvp in player.PlayerEquipment.EquippedItems)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value.Name}");
            }
        }
    }
}