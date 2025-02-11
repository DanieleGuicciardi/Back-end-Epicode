public class Employee
{
    private int Hour { get; set; }
    private string Job { get; set; }

    public Employee(int hour, string job)
    {
        Hour = hour;
        Job = job;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"He is working {Hour} hours as {Job}.");    
    }
    
}