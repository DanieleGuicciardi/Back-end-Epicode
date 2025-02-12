public class FindName
{
    public void SearchName()
    {
        string[] database = { "Daniele", "Giorgio", "Flavio", "Leonardo", "Simone" };
        Console.Write("Type the name to search in the database: ");
        string name = Console.ReadLine();

        int search = Array.IndexOf(database, name);

        if (search != -1)
        {
            Console.WriteLine("The name given is in the database!");
        }
        else
        {
            Console.WriteLine("Error, no name found.");
        }
    }
    
    
}
