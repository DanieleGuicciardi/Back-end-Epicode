using System;

public class BankAccount
{
    private int AccountNumber { get; set; }
    private string AccountName { get; set; }
    private decimal Balance { get; set; }
    private bool Active { get; set; }

    public BankAccount(string accountName)
    {
        AccountName = accountName;
        Balance = 0;
        Active = false;
    }

    public void CheckActive()
    {
        Console.Write("Type your account number: ");
        if (int.TryParse(Console.ReadLine(), out int accountNumber))
        {
            if (accountNumber == 1111)
            {
                Active = true;
                Console.WriteLine("Your account is now active.");
            }
            else
            {
                Console.WriteLine("Incorrect or non-existent account number!");
            }
        }
        else
        {
            Console.WriteLine("Please enter a valid number!");
        }
    }

    public void Deposit()
    {
        if (!Active)
        {
            Console.WriteLine("You must activate your account before depositing money.");
            return;
        }

        Console.Write("Enter the amount to deposit: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"You have deposited {amount} euros. Current balance: {Balance} euros.");
        }
        else
        {
            Console.WriteLine("Invalid amount!");
        }
    }

    public void Withdraw()
    {
        if (!Active)
        {
            Console.WriteLine("You must activate your account before withdrawing money.");
            return;
        }

        Console.Write("Enter the amount to withdraw: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"You have withdrawn {amount} euros. Current balance: {Balance} euros.");
            }
            else
            {
                Console.WriteLine("Insufficient funds!");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount!");
        }
    }

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\nWELCOME TO THE LITHUANIAN BANK");
            Console.WriteLine("------------------------------");
            Console.WriteLine("What would you like to do today, sir?");
            Console.WriteLine("Press 1 to open a new bank account");
            Console.WriteLine("Press 2 to deposit");
            Console.WriteLine("Press 3 to withdraw");
            Console.WriteLine("Press 4 to EXIT");

            if (int.TryParse(Console.ReadLine(), out int action))
            {
                if (action == 1)
                {
                    CheckActive();
                }
                else if (action == 2)
                {
                    Deposit();
                }
                else if (action == 3)
                {
                    Withdraw();
                }
                else if (action == 4)
                {
                    Console.WriteLine("Thank you for using our bank. Goodbye!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number!");
            }
            
            // Pause to prevent losing output
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
    }
}
