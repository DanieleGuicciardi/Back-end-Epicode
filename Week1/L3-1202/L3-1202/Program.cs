using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nChoose an exercise to run:");
            Console.WriteLine("1 - BankAccount");
            Console.WriteLine("2 - FindName");
            Console.WriteLine("3 - AverageAndSum");
            Console.WriteLine("4 - Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Digit your Account Name: ");
                string accountName = Console.ReadLine();
                BankAccount account = new BankAccount(accountName);
                account.Menu();
                break;
            }
            else if (choice == "2")
            {
                FindName finder = new FindName();
                finder.SearchName();
                break;
            }
            else if (choice == "3")
            {
                
                break;
            }
            else if (choice == "4")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice, try again.");
            }
        }
    }
}