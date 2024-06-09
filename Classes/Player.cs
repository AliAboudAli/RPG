namespace RpgGame.Classes;

public class Player
{
    PlayerClass playerClass;
    Race race;
    Elf  elf;
    Swordsman swordsman;
    Wizard wizard;
    public int Health { get; set; }
    public Player(PlayerClass playerClass, Race race, Elf elf, Swordsman swordsman, Wizard wizard)
    {
        this.playerClass = playerClass;
        this.race = race;
        this.elf = elf;
    }
    
    
}