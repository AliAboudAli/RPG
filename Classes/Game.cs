public class Game
{
    private Player player;
    private Location slenderForest;
    private Location senitCave;
    private Location mistyMountains;
    private Location azureLake;
    private Location forgottenRuins;
    private Location currentLocation;
    private Random random = new Random();
    private int currentEnemyIndex = 0;

    public static void Start()
    {
        Console.WriteLine("Welcome to TextRPG!");

        Game game = new Game(); // Create an instance of Game
        game.CreatePlayer();
        game.InitializeWorld();

        while (game.player.Health > 0)
        {
            game.Explore();
        }

        Console.WriteLine("Game over. Thanks for playing!");
    }

    private void CreatePlayer()
    {
        try
        {
            Console.Clear();
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Choose your race:");
            Console.WriteLine("1. Human");
            Console.WriteLine("2. Elf");
            Console.WriteLine("3. Dwarf");
            int raceChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            Race race = (Race)raceChoice;

            Console.Clear();
            Console.WriteLine("Choose your class:");
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Rogue");
            int classChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            CharacterClass playerClass = (CharacterClass)classChoice;

            player = new Player(name, race, playerClass);
            Console.WriteLine($"Welcome, {player.Name} the {player.Race} {player.Class}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void InitializeWorld()
    {
        // Initialize locations and their respective enemies
        Enemy[] slenderForestEnemies =
        {
            new Enemy("Goblin", Race.Human, CharacterClass.Warrior, 30, 8, 40),
            new Enemy("Wolf", Race.Elf, CharacterClass.Rogue, 40, 10, 50)
        };
        string slenderForestStory =
            "The Slender Forest is known for its eerie silence and towering slender trees. Many travelers have reported strange apparitions and ghostly whispers.";
        slenderForest = new Location("Slender Forest", "You find yourself in a dense forest.", slenderForestStory,
            slenderForestEnemies, new string[] { "You find a chest", "You see an old tree" });

        Enemy[] senitCaveEnemies =
        {
            new Enemy("Orc", Race.Dwarf, CharacterClass.Warrior, 50, 12, 60),
            new Enemy("Bat", Race.Human, CharacterClass.Mage, 20, 6, 30)
        };
        string senitCaveStory =
            "Senit Cave harbors secrets deep within its labyrinthine tunnels. Legends say it was once a refuge for a lost civilization.";
        senitCave = new Location("Senit Cave", "You enter Senit Cave, where shadows dance and whispers echo.",
            senitCaveStory, senitCaveEnemies,
            new string[] { "You find a crack in the wall", "You discover a hidden passage" });

        Enemy[] mistyMountainsEnemies =
        {
            new Enemy("Yeti", Race.Human, CharacterClass.Warrior, 60, 15, 80),
            new Enemy("Giant Eagle", Race.Elf, CharacterClass.Rogue, 50, 12, 70)
        };
        string mistyMountainsStory =
            "The Misty Mountains are home to ancient ruins and mythical creatures. Adventurers seek hidden treasures while avoiding dangers in the mist.";
        mistyMountains = new Location("Misty Mountains", "You climb the Misty Mountains, shrouded in thick mist.",
            mistyMountainsStory, mistyMountainsEnemies,
            new string[] { "You see an ancient ruin", "You find a strange inscription on a stone" });

        Enemy[] azureLakeEnemies =
        {
            new Enemy("Mermaid", Race.Elf, CharacterClass.Mage, 40, 10, 50),
            new Enemy("Water Spirit", Race.Dwarf, CharacterClass.Rogue, 45, 11, 55)
        };
        string azureLakeStory =
            "Azure Lake is a sparkling, crystal-clear place surrounded by mystery. It's said that the water has magical properties and harbors rare creatures.";
        azureLake = new Location("Azure Lake", "You arrive at Azure Lake, where the water is clear and calm.",
            azureLakeStory, azureLakeEnemies,
            new string[] { "You find a mysterious bottle in the water", "You see a glowing fish" });

        Enemy[] forgottenRuinsEnemies =
        {
            new Enemy("Ghost", Race.Human, CharacterClass.Mage, 35, 9, 45),
            new Enemy("Possessed Statue", Race.Dwarf, CharacterClass.Warrior, 55, 14, 65)
        };
        string forgottenRuinsStory =
            "The Forgotten Ruins are filled with ancient relics and mystical energies. It's a place where the past and present converge.";
        forgottenRuins = new Location("Forgotten Ruins",
            "You enter the Forgotten Ruins, where the air is thick with magic.", forgottenRuinsStory,
            forgottenRuinsEnemies, new string[] { "You discover an ancient tablet", "You hear strange sounds" });

        // Start the player in Slender Forest
        currentLocation = slenderForest;
        Console.WriteLine("Your adventure begins in the Slender Forest.");
    }

    private void Explore()
    {
        Console.WriteLine(currentLocation.Description);
        Console.WriteLine(currentLocation.Story);
        Console.WriteLine(currentLocation.Description);
        Console.WriteLine(currentLocation.Story);

        int encounterChance = random.Next(0, 100);
        if (encounterChance < 50)
        {
            if (currentEnemyIndex < currentLocation.Enemies.Length)
            {
                Enemy enemy = currentLocation.Enemies[currentEnemyIndex];
                Console.WriteLine($"You encounter a {enemy.Name}!");
                Combat(enemy);
                currentEnemyIndex++;
            }
            else
            {
                Console.WriteLine("There are no more enemies in this location.");
            }
        }
        else
        {
            string eventDescription =
                currentLocation.NonEncounterEvents[random.Next(0, currentLocation.NonEncounterEvents.Length)];
            Console.WriteLine(eventDescription);
        }

        Console.WriteLine("What would you like to do now?");
        Console.WriteLine("1. Move to another location");
        Console.WriteLine("2. Continue exploring");
        Console.WriteLine("3. Check your status");

        try
        {
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (choice)
            {
                case 1:
                    ChangeLocation();
                    break;
                case 2:
                    Explore();
                    break;
                case 3:
                    ShowStatus();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void ChangeLocation()
    {
        Console.WriteLine("Choose a new location:");
        Console.WriteLine("1. Slender Forest");
        Console.WriteLine("2. Senit Cave");
        Console.WriteLine("3. Misty Mountains");
        Console.WriteLine("4. Azure Lake");
        Console.WriteLine("5. Forgotten Ruins");

        try
        {
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (choice)
            {
                case 1:
                    currentLocation = slenderForest;
                    break;
                case 2:
                    currentLocation = senitCave;
                    break;
                case 3:
                    currentLocation = mistyMountains;
                    break;
                case 4:
                    currentLocation = azureLake;
                    break;
                case 5:
                    currentLocation = forgottenRuins;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    ChangeLocation();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void ShowStatus()
    {
        Console.WriteLine($"Name: {player.Name}");
        Console.WriteLine($"Race: {player.Race}");
        Console.WriteLine($"Class: {player.Class}");
        Console.WriteLine($"Level: {player.Level}");
        Console.WriteLine($"Experience: {player.Experience}");
        Console.WriteLine($"Health: {player.Health}");
        Console.WriteLine($"Mana: {player.Mana}");
        Console.WriteLine($"Attack Power: {player.AttackPower}");
        Console.WriteLine($"Special Attacks Left: {player.SpecialAttacksLeft}");
    }

    private void Combat(Enemy enemy)
    {
        while (enemy.Health > 0 && player.Health > 0)
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Special Attack");
            Console.WriteLine("3. Heal");

            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        player.Attack(enemy);
                        break;
                    case 2:
                        player.SpecialAttack(enemy);
                        break;
                    case 3:
                        player.Heal(20); // Example: Heal the player for 20 health points
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                if (enemy.Health > 0)
                {
                    enemy.Attack(player);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Check if enemy is defeated
        if (enemy.Health <= 0)
        {
            Console.WriteLine($"You have defeated the {enemy.Name}!");

            // Check if all enemies in all locations are defeated
            if (AllEnemiesDefeated())
            {
                Console.WriteLine("Congratulations! You have defeated all enemies and completed your adventure.");
                Console.WriteLine("Thank you for playing TextRPG!");
                player.Health = 0; // End the game
            }
            else
            {
                Console.WriteLine("You have defeated the enemies in this location.");
                Console.WriteLine("You can continue exploring or move to another location.");
                currentEnemyIndex = 0; // Reset enemy index for next location
            }
        }
    }

    private bool AllEnemiesDefeated()
    {
        bool allDefeated = true;

        // Check each location for remaining enemies
        if (!AreEnemiesDefeated(slenderForest))
            allDefeated = false;
        if (!AreEnemiesDefeated(senitCave))
            allDefeated = false;
        if (!AreEnemiesDefeated(mistyMountains))
            allDefeated = false;
        if (!AreEnemiesDefeated(azureLake))
            allDefeated = false;
        if (!AreEnemiesDefeated(forgottenRuins))
            allDefeated = false;

        return allDefeated;
    }

    private bool AreEnemiesDefeated(Location location)
    {
        foreach (Enemy enemy in location.Enemies)
        {
            if (enemy.Health > 0)
                return false;
        }

        return true;
    }
}
