using System;

namespace RpgGame.Classes
{
    public class Swordsman : PlayerClass
    {
        public Swordsman()
        {
            ClassName = "Zwaardvechter";
        }

        public override void SpecialAttack()
        {
            int specialAttackDamage = Attack * 3;
            Console.WriteLine($"Uitvoeren van speciale aanval als een Zwaardvechter. Schade: {specialAttackDamage}");
        }
    }
}