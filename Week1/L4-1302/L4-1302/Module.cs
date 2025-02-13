public class Module
{
    private string Username;

    public Module(string username, string password)
    {
        Username = username;
    }

    private void Login()
    {
        Console.WriteLine("Digit your username: ");
        string username = Console.ReadLine();
        string[] userDatabase = { "Danieloun", "Valerio", "Simone", "Giorgio" };
        int search = Array.IndexOf(userDatabase, username);

        if (search != -1)
        {
            Console.WriteLine("Digit your password: ");
            string password = Console.ReadLine();

            if (password == "1234")
            {
                Console.WriteLine("User logged");
            }
            else
            {
                Console.WriteLine("Password doesn t match che username");
                Console.WriteLine("Enter to retry");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("This user do not exist");
            Console.WriteLine("Enter to retry");
            Console.ReadLine();
        }
        
        
    }

    public void function()
    {
        while (true)
        {
            Console.WriteLine("===============OPERAZIONS==============");
            Console.WriteLine("Hello sir, what you wanna do today?");
            Console.WriteLine("1.: Login");
            Console.WriteLine("2.: Logout");
            Console.WriteLine("3.: Check date and login hour");
            Console.WriteLine("4.: Access info");
            Console.WriteLine("5.: Exit");
            Console.WriteLine("========================================");

            int x = int.Parse(Console.ReadLine());

            if (x == 1)
            {
                Login();
            }
            else if (x == 2)
            {
                
            }
            else if (x == 3)
            {
                
            }
            else if (x == 4)
            {
                
            }
            else if (x == 5)
            {
                Console.WriteLine("Have a nice day!");
                break;
            }
            else
            {
                Console.WriteLine("Error, invalid input.");
            }
        }
    }
}