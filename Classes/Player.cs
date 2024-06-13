public enum Race
{
    Human,
    Elf,
    Dwarf
}

public enum CharacterClass
{
    Warrior,
    Mage,
    Rogue
}
public class Player
{
    public string Name { get; set; }
    public Race Race { get; set; }
    public CharacterClass Class { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }
    public int AttackPower { get; set; }
    public int SpecialAttacksLeft { get; set; }

    public Player(string name, Race race, CharacterClass playerClass)
    {
        Name = name;
        Race = race;
        Class = playerClass;
        Level = 1;
        Experience = 0;
        Health = 100;
        Mana = 100;
        AttackPower = 10;
        SpecialAttacksLeft = 3;
    }
    public void Heal(int amount)
    {
        Health += amount;
        Console.WriteLine($"{Name} healed for {amount} health.");
    }

    public void LevelUp()
    {
        Level++;
        Health += 20;
        AttackPower += 5;
        Mana += 20;
        Console.WriteLine($"Congratulations! You have leveled up to level {Level}.");
    }

    public void GainExperience(int amount)
    {
        Experience += amount;
        if (Experience >= 100)
        {
            LevelUp();
            Experience -= 100;
        }
    }

    public void Attack(Enemy enemy)
    {
        int damage = AttackPower;
        enemy.Health -= damage;
        Console.WriteLine($"You attack the {enemy.Name} and deal {damage} damage!");

        if (enemy.Health <= 0)
        {
            Console.WriteLine($"You have defeated the {enemy.Name}!");
            GainExperience(enemy.ExperienceValue);
        }
    }

    public void SpecialAttack(Enemy enemy)
    {
        if (SpecialAttacksLeft > 0)
        {
            int damage = 50;
            enemy.Health -= damage;
            Console.WriteLine($"You use a special attack on the {enemy.Name} and deal {damage} damage!");

            SpecialAttacksLeft--;
            if (enemy.Health <= 0)
            {
                Console.WriteLine($"You have defeated the {enemy.Name} with a special attack!");
                GainExperience(enemy.ExperienceValue);
            }
        }
        else
        {
            Console.WriteLine("You have no special attacks left!");
        }
    }
}