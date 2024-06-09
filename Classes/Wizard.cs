using System;

namespace RpgGame.Classes
{
    public class Wizard : PlayerClass
    {
        public Wizard()
        {
            ClassName = "Tovenaar";
        }

        public override void SpecialAttack()
        {
            int specialAttackDamage = Attack * 4;
            Console.WriteLine($"Uitvoeren van speciale aanval als een Tovenaar. Schade: {specialAttackDamage}");
        }
    }
}