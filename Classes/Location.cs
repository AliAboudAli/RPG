public class Location
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Story { get; private set; }
    public Enemy[] Enemies { get; private set; }
    public string[] NonEncounterEvents { get; private set; }

    public Location(string name, string description, string story, Enemy[] enemies, string[] nonEncounterEvents)
    {
        Name = name;
        Description = description;
        Story = story;
        Enemies = enemies;
        NonEncounterEvents = nonEncounterEvents;
    }
}