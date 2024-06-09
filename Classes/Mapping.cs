﻿namespace RpgGame.Classes
{
    public class Mapping
    {
        // Dictionary that maps locations to lists of enemies.
        readonly Dictionary<string, List<Enemy>> locationEnemies;
        // List of all available locations.
        readonly List<string> locations;
        public Player user;
        public Combat Combat { get; private set; }
        public string CurrentLocation { get; set; }

        private Random random = new Random();

        // Valid inputs for navigation commands.
        private string[] validInputs =
        {
            "Travel", "Cancel", "Forest", "Dungeon", "Valley", "Temple", "Comfy City", "City of Awakening"
        };

        // Constructor initializes the class with a player.
        public Mapping(Player User)
        {
            user = User;
            locationEnemies = new Dictionary<string, List<Enemy>>
            {
                // Initialization of enemies per location.
                {
                    "Comfy City",
                    new List<Enemy>
                    {
                        new Enemy("Growling Hound", 100, "Growling Hound behavior", "Comfy City"),
                        new Enemy("Witch", 100, "Witch behavior", "Comfy City")
                    }
                },
                {
                    "Scary Forest",
                    new List<Enemy>
                    {
                        new Enemy("Orcs", 100, "Orcs behavior", "Scary Forest"),
                        new Enemy("Werewolves", 100, "Werewolves behavior", "Scary Forest")
                    }
                },
                {
                    "Mountain Dungeon",
                    new List<Enemy>
                    {
                        new Enemy("Corrupted Soldier", 100, "Corrupted Soldier behavior", "Mountain Dungeon"),
                        new Enemy("Skeletal Warrior", 100, "Skeletal Warrior behavior", "Mountain Dungeon")
                    }
                },
                {
                    "Scorpion Valley",
                    new List<Enemy>
                        { new Enemy("Poisonous Scorpion", 100, "Poisonous Scorpion behavior", "Scorpion Valley") }
                },
                {
                    "Forgotten Temple",
                    new List<Enemy> { new Enemy("Wizard", 100, "Wizard behavior", "Forgotten Temple") }
                },
                {
                    "City of Awakening",
                    new List<Enemy>
                    {
                        new Enemy("Werewolves", 100, "Werewolves behavior", "City of Awakening"),
                        new Enemy("Witch", 100, "Witch behavior", "City of Awakening")
                    }
                },
            };

            // Store location names in the list.
            locations = new List<string>(locationEnemies.Keys);
        }
        
        // Checks input
        private int GetValidInput(int minValue, int maxValue)
        {
            int choice;
            while (true)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= minValue && choice <= maxValue)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice.");
                }
            }
        }

        // Starts the story by inviting the player to travel.
        public void StartStory()
        {
            Console.WriteLine("Welcome to ShrekLand");
            Travel();
        }

        public void Encounter()
        {
            // Checks if enemies can spawn in a random format, then they can spawn with a 50% chance
            if (ShouldEncounterEnemies(CurrentLocation))
            {
                Console.WriteLine($"You encounter enemies in {CurrentLocation}!");
                FightEnemies(locationEnemies[CurrentLocation]);
            }
        }

        private void FightEnemies(List<Enemy> enemies)
        {
            Console.WriteLine("Battle Start!");

            // Loop through the enemies to play
            foreach (var enemy in enemies)
            {
                while (enemy.Health > 0 && user.Health > 0)
                {
                    // Enemy attacks
                    Console.WriteLine($"{enemy.Behaviour} is attacking you.");

                    // Show combat information.
                    Console.WriteLine("Combat Options:");
                    Combat.CombatOptions();

                    int choice = GetValidInput(1, 5);

                    switch (choice)
                    {
                        case 1:
                           // Check for player attack 
                            Console.WriteLine("You chose to attack!");
                            Combat.DoAttack(enemy);
                            break;
                        case 2:
                            // Check for player defense 
                            Console.WriteLine("You chose to defend!");
                            Combat.DoDefend(user);
                            break;
                        case 3:
                            // Check for player healing 
                            Console.WriteLine("You chose to heal!");
                            Console.WriteLine("Health healed");
                            Combat.DoHeal(user);
                            break;
                        case 4:
                            // Check for player special attack 
                            Console.WriteLine("You chose to perform a special attack!");
                            Combat.DoSpecialAttack(enemy);
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }

                    // Check if the player is null
                    if (user.Health <= 0)
                    {
                        // Then the player dies and the game is over with the enemy's behavior, such as name
                        Console.WriteLine($"Game Over! You were defeated by the {enemy.Behaviour}");
                        return;
                    }
                }
            }
        }
        // Manages the player's travel process between locations.
        public void Travel()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Where would you like to travel?");
            DisplayLocations();

            Console.WriteLine("Enter the initial letters of the location you want to travel to:");

            while (true)
            {
                var userInput = Console.ReadLine().Trim().ToLower();

                // Check for canceling travel
                if (userInput == "cancel")
                {
                    Console.WriteLine("Travel canceled.");
                    return;
                }

                // Looks for a new valid input
                var selectedLocation = PreprocessInput(userInput);

                // Check if it's in the list of locations
                if (validInputs.Contains(selectedLocation))
                {
                    // Go to the location, watch out for enemies!
                    MoveToLocation(selectedLocation);

                    break;
                }
                else
                {
                    // Error message
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid location. Please enter more specific initials or 'cancel' to cancel.");
                    Console.ResetColor();
                }
            }
            // Check if the player has a chance to fight the enemy in the location
            if (ShouldEncounterEnemies(CurrentLocation))
            {
                Console.WriteLine($"You encounter enemies in {CurrentLocation}!");
                FightEnemies(locationEnemies[CurrentLocation]);
            }
        }

        private void MoveToLocation(string selectedLocation)
        {
            Console.WriteLine($"Moving to {selectedLocation}...");

            // Update the current location
            CurrentLocation = selectedLocation;

            // Check if the player still has a chance to fight
            if (ShouldEncounterEnemies(selectedLocation))
            {
                Console.WriteLine($"You encounter enemies in {selectedLocation}!");
                FightEnemies(locationEnemies[selectedLocation]);
            }
            else
            {
                // Otherwise the game continues
                ExamineLocation(selectedLocation);
            }
        }
        // Method for output if the player wants to continue
        
        private void ExamineLocation(string location)
        {
            Console.WriteLine($"You are at {location}. What would you like to do?");
            Console.WriteLine("1. Continue exploring");
            Console.WriteLine("2. Return to main menu");

            var userInput = Console.ReadLine().Trim().ToLower();

            if (userInput == "1" || userInput == "explore")
            {
                if (random.Next(0, 2) == 0)
                {
                    Console.WriteLine("You found a chest!");

                    int gold = random.Next(10, 101);
                    int potion = random.Next(1, 4);
                    int xp = random.Next(20, 51);

                    Console.WriteLine($"You found {gold} gold coins!");
                    Console.WriteLine($"You found {potion} potions!");
                    Console.WriteLine($"You gained {xp} experience points!");

                    user.PlayerCurrency.Gold += gold;
                    user.Experience += xp;
                }
                else
                {
                    Console.WriteLine("You continue exploring but find nothing of interest.");
                }
            }
            else if (userInput == "2" || userInput == "return")
            {
                Console.WriteLine("Returning to main menu...");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        private void DisplayLocations()
        {
            var key = 'W';
            foreach (var location in locations)
            {
                Console.WriteLine($"{key}. {location}");
                key++;
            }

            Console.ResetColor();
        }

        public bool ShouldEncounterEnemies(string location)
        {
            return random.Next(0, 2) == 0 && locationEnemies.ContainsKey(location);
        }

        private string PreprocessInput(string input)
        {
            // Check if the input matches any valid location names
            foreach (var validInput in validInputs)
            {
                // Check if the input matches the beginning of a valid location name
                if (validInput.ToLower().StartsWith(input))
                {
                    // If a match is found, return the full valid location name
                    return validInput;
                }
            }

            // If no match is found, return the original input
            return input;
        }
    }
}