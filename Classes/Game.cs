namespace RpgGame.Classes;
public class Game
{
    private int seconds = 5;

    public void MenuStart()
    {
        // Toont het hoofdmenu van het spel.
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
                Console.Write("Incorrect input, try again");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Red;
                break;
        }

        Console.Clear();
    }

    private int GetValidInput(int minValue, int maxValue)
    {
        int chosing;
        while (true)
        {
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out chosing) && chosing >= minValue && chosing <= maxValue)
            {
                Console.WriteLine("You chose: " + chosing);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
            }
        }
    } 
    
    // Voert de hoofdlogica van het spel uit.
    public void RunGame()
    {
        Console.Clear();

        // Creëer een nieuwe spelerinstantie.
        Speler player = new Speler();

        // Verkrijg de spelersnaam.
        Console.WriteLine("Please enter your nickname:");
        player.Name = Console.ReadLine();
        Console.Clear();
        

        // Laat de speler een klasse kiezen.
        player.ChoiceClass();

        // Stel het initiële niveau van de speler in op 1.
        player.Level = 1;

        // Voeg items toe aan de inventaris.
        Item healthPotion = new Item { Name = "Health Potion", Price = 10 };

        // Rust items uit.
        player.PlayerEquipment.EquipItem(healthPotion, EquipmentSlot.Chest);
        player.PlayerEquipment.EquipItem(healthPotion, EquipmentSlot.Potion);

        Mapping map = new Mapping(player);

        // Instantieer de winkel.
        Shop shop = new Shop();
        
        map.StartStory();

        // Stel de beginlocatie in op Comfy City.
        map.CurrentLocation = "Comfy City";

        // Start de hoofdlus voor spelersacties.
        while (true)
        {
            Console.WriteLine($"You are currently in: {map.CurrentLocation}");
            Console.WriteLine("1. Travel to a new location");
            Console.WriteLine("2. Visit the shop");
            Console.WriteLine("3. Display player information");

            int choice = GetValidInput(1, 3);

            switch (choice)
            {
                case 1:
                    // Travel to a new location
                    //map.Travel();
                    Console.WriteLine("Going to Travel method");
                    // Encounter while traveling
                    //map.Encounter();
                    break;
                case 2:
                    // Naar Shop.
                    Console.Clear();
                    Console.WriteLine("Welcome to the shop!");
                    shop.DisplayItems();
                    shop.BuyItem(player, healthPotion);
                    shop.SellItem(player, healthPotion);
                    break;
                case 3:
                    // Speler informatie.
                    DisplayPlayerInfo(player);
                    break;
            }

            // Controleer speler levens is 0
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

            // Toon gevechtsopties na elke actie.
            if (choice != 3) 
            {
                //Toon combat opties.
                Console.WriteLine("Combat Options:");
                map.Combat.CombatOptions();
                switch (choice)
                {
                    case 1:
                        // Nieuwe locatie gaan.
                        map.Travel();
                        // Laat vijanden je aanvallen.
                        map.Encounter();
                        break;
                    case 2:
                        // Naar shop.
                        Console.Clear();
                        Console.WriteLine("Welcome to the shop!");
                        shop.DisplayItems();
                        shop.BuyItem(player, healthPotion);
                        shop.SellItem(player, healthPotion);
                        break;
                    case 3:
                        // Toon speler informaties.
                        DisplayPlayerInfo(player);
                        break;
                }
            }
        }
    }

    // Toon gedetailleerde informatie over de speler.
    public void DisplayPlayerInfo(Speler player)
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

public class Options
{
    public ConsoleColor selectedForegroundColor { get; set; } = ConsoleColor.White;
    public ConsoleColor selectedBackgroundColor { get; set; } = ConsoleColor.Black;

    // Toont het optiemenu.
    public void OptionMenu()
    {
        Console.Clear();
        Console.WriteLine("Enter an option:");
        Console.WriteLine("1. Help");
        Console.WriteLine("2. Option Color");

        if (int.TryParse(Console.ReadLine(), out int option))
        {
            switch (option)
            {
                case 1:
                    OptionHelp();
                    break;
                case 2:
                    OptionsSelect();
                    break;
                default:
                    Console.WriteLine("Invalid option selected");
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Vraag om terug te gaan.
        Console.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        Console.ResetColor();
    }

    // Toont hulpinformatie voor het spel.
    public void OptionHelp()
    {
        Console.Clear();
        Console.WriteLine("Helpful information about how the game works goes here.");
        Console.WriteLine("This is a simple text-based RPG game.");
        Console.WriteLine("You can choose a character (Wizard, Swordsman, or Elf) and customize the console color.");

        // Vraag om terug te gaan.
        Console.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        Console.ResetColor();
        OptionMenu();
    }

    // Stelt de gebruiker in staat om de consolekleur aan te passen.
    public void OptionsSelect()
    {
        Console.Clear();
        Console.WriteLine("Customize your Console: ");
        Console.WriteLine("0 -> Blue");
        Console.WriteLine("1 -> Green");
        Console.WriteLine("2 -> Yellow");
        Console.WriteLine("3 -> White");
        Console.WriteLine("4 -> Grey");
        Console.WriteLine("5 -> Purple");

        if (int.TryParse(Console.ReadLine(), out int selectedColor))
        {
            var type = typeof(ConsoleColor);

            if (Enum.IsDefined(type, selectedColor))
            {
                selectedForegroundColor = (ConsoleColor)Enum.ToObject(type, selectedColor);
                Console.ForegroundColor = selectedForegroundColor;

                // Pas kleurinstellingen toe.
                ApplyColorSettings();
            }
            else
            {
                Console.WriteLine("Invalid color option selected.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }

        // Vraag om terug te gaan.
        Console.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
        Console.ResetColor();
        OptionMenu();
    }

    // Past de geselecteerde kleurinstellingen toe.
    public void ApplyColorSettings()
    {
        Console.ForegroundColor = selectedForegroundColor;
        Console.BackgroundColor = selectedBackgroundColor;
    }
}