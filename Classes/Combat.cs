using System;

namespace RpgGame.Classes
{
    public class Combat
    {
        // Een readonly instance van Random om te zorgen voor willekeurige uitkomsten in het spel.
        private readonly Random random = new Random();

        // Eigenschappen van de speler in het gevecht.
        public string Name { get; set; }
        public string Class { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int AttackChance { get; set; }
        public int Defense { get; set; }
        public int DefenseChance { get; set; }
        public int Heal { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        // Referentie naar de spelerobject en het spelobject.
        private Speler user;
        private Game game = new Game();

        // Constructor om het Combat object te initialiseren met de meegegeven waarden.
        public Combat(string name, string characterClass, int health, int attack, int attackChance, int defense,
            int defenseChance, int heal, int minDamage, int maxDamage)
        {
            Name = name;
            Class = characterClass;
            Health = health;
            Attack = attack;
            AttackChance = attackChance;
            Defense = defense;
            DefenseChance = defenseChance;
            Heal = heal;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        // Weergeeft de combat-opties op basis van de klasse van de speler.
        public void CombatOptions()
        {
            Console.WriteLine("=========================");
            Console.WriteLine($"+ Attack(1) - Defense(2) - Heal(3) +");
            switch (Class.ToLower())
            {
                case "wizard":
                    Console.WriteLine("+ Fireball(4) - Run(5) +");
                    break;
                case "swordsman":
                    Console.WriteLine("+ Powerful Strike(4) - Run(5) +");
                    break;
                case "elf":
                    Console.WriteLine("+ Magical Spell(4) - Run(5) +");
                    break;
            }
            Console.WriteLine("Choose your command!");
        }

        // Verdedigingsmechanisme van de speler.
        public void DoDefend(Speler player)
        {
            Console.WriteLine("You chose to defend!");

            int defenseRoll = random.Next(1, 101); // Kansberekening voor verdediging.

            if (defenseRoll <= DefenseChance)
            {
                Console.WriteLine("You successfully defended against the enemy's attack!");
            }
            else
            {
                Console.WriteLine("Your defense failed!");
                int damage = random.Next(10, 21); // Berekent de schade.
                Health -= damage;

                if (Health <= 0)
                {
                    Die(); // Speler sterft als gezondheid <= 0.
                }
                else
                {
                    Console.WriteLine($"You took {damage} damage. Your current health: {Health}");
                }
            }

            CombatOptions(); // Terug naar het gevechtopties menu.
        }

        // Voert een speciale aanval uit.
        public void DoSpecialAttack(Enemy enemy)
        {
            Console.WriteLine($"Special attack used at health: {Health}");
        }

        // Aanval door de speler.
        public void DoAttack(Enemy enemy)
        {
            int attackRoll = random.Next(20, 80);
            Console.WriteLine($"Attack Roll: {attackRoll}, Attack Chance: {AttackChance}");

            if (attackRoll <= AttackChance)
            {
                int damage = random.Next(MinDamage, MaxDamage + 1);
                enemy.Health -= damage;
                Console.WriteLine($"You dealt {damage} damage to {enemy.Name}!");

                if (enemy.Health <= 0)
                {
                    Die(); // Als de gezondheid van de vijand <= 0, speler wint.
                }
            }
        }

        // Behandelt de speler's dood en herstart het spel.
        public void Die()
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("           GAME OVER              ");
            Console.WriteLine("=================================");
            Console.WriteLine($"Player {Name} was defeated.");
            Console.WriteLine($"Final Health: {Health}");
            Console.WriteLine("=================================");
            Console.WriteLine("Press any key to go back to the main menu");
            Console.ReadKey();
            game.MenuStart(); // Gaat terug naar het hoofdmenu.
        }
    }
}
