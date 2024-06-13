public class Enemy
{
    public string Name { get; set; }
    public Race Race { get; set; }
    public CharacterClass Class { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public int ExperienceValue { get; set; }

    public Enemy(string name, Race race, CharacterClass enemyClass, int health, int attackPower, int experienceValue)
    {
        Name = name;
        Race = race;
        Class = enemyClass;
        Health = health;
        AttackPower = attackPower;
        ExperienceValue = experienceValue;
    }

    public void Attack(Player player)
    {
        int damage = AttackPower;
        player.Health -= damage;
        Console.WriteLine($"The {Name} attacks you and deals {damage} damage!");

        if (player.Health <= 0)
        {
            Console.WriteLine("You have been defeated...");
            Environment.Exit(0);
        }
    }
}