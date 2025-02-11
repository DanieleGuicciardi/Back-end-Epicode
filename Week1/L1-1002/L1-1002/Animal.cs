public class Animal
{ 
    private string Species { get; set; }

    public Animal(string species)
    {
        Species = species;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"He have a {Species}.");
    }
}