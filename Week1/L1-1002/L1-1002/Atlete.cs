public class Atlete
{
    private string Name { get; set; }
    private string Sport { get; set; }
    private string Gender { get; set; }

    public Atlete(string name, string sport, string gender)
    {
        Name = name;
        Sport = sport;
        Gender = gender;
    }

    public void PrintInfo()
    {
        string pronoun = (Gender.ToUpper() == "M") ? "he" : "she";
        Console.WriteLine($"The athlete's name is {Name}, and {pronoun} is a {Sport}.");
    }
}