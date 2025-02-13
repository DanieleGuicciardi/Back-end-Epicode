public class Login
{
    private string Username;
    private string Password;

    public Login(string username, string password)
    {
        Username = username;
        Password = password;
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
                Console.WriteLine("Sei dentro 1");
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