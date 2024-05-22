

namespace RpgGame.Classes
{
    public class Mapping
    {
        // Dictionary die locaties koppelt aan lijsten van vijanden.
        readonly Dictionary<string, List<Enemy>> locationEnemies;
        // Lijst met alle beschikbare locaties.
        readonly List<string> locations;
        public Speler user;
        public Combat Combat { get; private set; }
        public string CurrentLocation { get; set; }

        private Random random = new Random();

        // Geldige inputs voor navigatiecommando's.
        private string[] validInputs =
        {
            "Travel", "Cancel", "Forest", "Dungeon", "Valley", "Temple", "Comfy City", "City of Awakening"
        };

        // Constructor initialiseert de klasse met een speler.
        public Mapping(Speler User)
        {
            user = User;
            Combat = new Combat(user.Name, user.Class, user.Health, user.Attack, user.AttackChance, user.Defense,
                user.DefenseChance, user.Heal, user.MinDamage, user.MaxDamage);
            locationEnemies = new Dictionary<string, List<Enemy>>
            {
                // Initialisatie van vijanden per locatie.
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

            // Locatienamen in de lijst opslaan.
            locations = new List<string>(locationEnemies.Keys);
        }
        
        //Controleert input
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

        // Start het verhaal door de speler uit te nodigen om te reizen.
        public void StartStory()
        {
            Story story = new Story(user.Name);
            Travel();
        }

        public void Encounter()
        {
            // Controleert of enemies kunnen spawnen in random formatie dan kan die met 50 procent spawnen
            if (ShouldEncounterEnemies(CurrentLocation))
            {
                Console.WriteLine($"You encounter enemies in {CurrentLocation}!");
                FightEnemies(locationEnemies[CurrentLocation]);
            }
        }

        private void FightEnemies(List<Enemy> enemies)
        {
            Console.WriteLine("Battle Start!");

            // Loop door de vijanden te spelen
            foreach (var enemy in enemies)
            {
                while (enemy.Health > 0 && user.Health > 0)
                {
                    // Enemy valt aan
                    Console.WriteLine($"{enemy.Behaviour} is attacking you.");

                    // Toon gevecht infomatie.
                    Console.WriteLine("Combat Options:");
                    Combat.CombatOptions();

                    int choice = GetValidInput(1, 5);

                    switch (choice)
                    {
                        case 1:
                           //controle for aanval speler 
                            Console.WriteLine("You chose to attack!");
                            Combat.DoAttack(enemy);
                            break;
                        case 2:
                            //controle for verdedigen speler 
                            Console.WriteLine("You chose to defend!");
                            Combat.DoDefend(user);
                            break;
                        case 3:
                            //controle for healen speler 
                            Console.WriteLine("You chose to heal!");
                            Console.WriteLine("Health healed");
                            break;
                        case 4:
                            //controle for speciale aanval speler 
                            Console.WriteLine("You chose to perform a special attack!");
                            Combat.DoSpecialAttack(enemy);
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }

                    // Controleer speler die null is
                    if (user.Health <= 0)
                    {
                        //dan speler gaat dood en game over met de vijands gedrag o.a naam
                        Console.WriteLine($"Game Over! You were defeated by the {enemy.Behaviour}");
                        return;
                    }
                }
            }
        }
        // Beheert reisprocessen van de speler tussen locaties.
        public void Travel()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Where would you like to travel?");
            DisplayLocations();

            Console.WriteLine("Enter the initial letters of the location you want to travel to:");

            while (true)
            {
                var userInput = Console.ReadLine().Trim().ToLower();

                // Controle voor afbreken om door te reizen
                if (userInput == "cancel")
                {
                    Console.WriteLine("Travel canceled.");
                    return;
                }

                // kijkt voor een nieuwe geldige iput
                var selectedLocation = PreprocessInput(userInput);

                // controle of die in de lijst van locaties bevatten
                if (validInputs.Contains(selectedLocation))
                {
                    // op naar de locatie op passen voor enemies!
                    MoveToLocation(selectedLocation);

                    break;
                }
                else
                {
                    //fout melding
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid location. Please enter more specific initials or 'cancel' to cancel.");
                    Console.ResetColor();
                }
            }
            // Controleren of de speler in de locatie een kans heeft om de vijand te vechten
            if (ShouldEncounterEnemies(CurrentLocation))
            {
                Console.WriteLine($"You encounter enemies in {CurrentLocation}!");
                FightEnemies(locationEnemies[CurrentLocation]);
            }
        }

        private void MoveToLocation(string selectedLocation)
        {
            Console.WriteLine($"Moving to {selectedLocation}...");

            // Update de huidige locatie
            CurrentLocation = selectedLocation;

            // controleert de speler nog een kans om te vechten
            if (ShouldEncounterEnemies(selectedLocation))
            {
                Console.WriteLine($"You encounter enemies in {selectedLocation}!");
                FightEnemies(locationEnemies[selectedLocation]);
            }
            else
            {
                // Anders gaat de spel door 
                ExamineLocation(selectedLocation);
            }
        }
        // methode voor output als de speler door wilt gaan 
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
