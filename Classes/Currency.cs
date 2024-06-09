using System;

namespace RpgGame.Classes
{
    public class Currency
    {
        public int Gold { get; set; }

        public Currency()
        {
            Gold = 0;
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"Toegevoegd {amount} goud aan de valuta van de speler.");
        }

        public void SubtractGold(int amount)
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                Console.WriteLine($"Afgetrokken {amount} goud van de valuta van de speler.");
            }
            else
            {
                Console.WriteLine("Niet genoeg goud om de transactie uit te voeren.");
            }
        }
    }
}