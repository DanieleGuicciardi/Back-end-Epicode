public class Car
{
    private string Brand { get; set; }
    private string Name { get; set; }

    public Car(string brand, string name)
    {
        Brand = brand;
        Name = name;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"I have a {Brand}, {Name}.");
    }
}