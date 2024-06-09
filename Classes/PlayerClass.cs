using System;

namespace RpgGame.Classes
{
    public abstract class PlayerClass
    {
        public int Attack { get; set; }
        public string ClassName { get; set; }
        public int SpecialAttackDamage { get; set; }
        public int SpecialAttackUses { get; set; }

        protected PlayerClass()
        {
            Attack = 10;
            ClassName = "";
            SpecialAttackDamage = 0;
            SpecialAttackUses = 2; // Speler kan twee keer de speciale aanval gebruiken
        }

        public abstract void SpecialAttack();

        public void UseSpecialAttack()
        {
            if (SpecialAttackUses > 0)
            {
                SpecialAttack();
                SpecialAttackUses--;
                Console.WriteLine($"Je hebt de speciale aanval gebruikt! Je hebt nog {SpecialAttackUses} keer over.");
            }
            else
            {
                Console.WriteLine("Je hebt geen speciale aanvallen meer over.");
            }
        }
    }
}