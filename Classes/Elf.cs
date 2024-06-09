using System;

namespace RpgGame.Classes
{
    public class Elf : PlayerClass
    {
        public Elf()
        {
            ClassName = "Elf";
        }

        public override void SpecialAttack()
        {
            int specialAttackDamage = Attack * 2;
            Console.WriteLine($"Uitvoeren van speciale aanval als een Elf. Schade: {specialAttackDamage}");
        }
    }
}