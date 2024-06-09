using RpgGame.Classes;

public class Race
{
    public string Name { get; set; }
    public int HealthModifier { get; set; }
    public int AttackModifier { get; set; }
    public int DefenseModifier { get; set; }

    public Race(string name, int healthModifier, int attackModifier, int defenseModifier)
    {
        Name = name;
        HealthModifier = healthModifier;
        AttackModifier = attackModifier;
        DefenseModifier = defenseModifier;
    }
}