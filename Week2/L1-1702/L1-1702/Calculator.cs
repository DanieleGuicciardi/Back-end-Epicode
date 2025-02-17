using System;

public class Calculator
{
    private double Balance { get; set; }

    public Calculator()
    {
        Balance = 0;    
    }

    public void Function()
    {
        while (true) // Loop infinito
        {
            Console.WriteLine("What would you like to eat? Type the number (or type 0 to exit): ");
            
            if (int.TryParse(Console.ReadLine(), out int x))
            {
                if (x == 1)
                {
                    Balance += 2.50;
                }
                else if (x == 2)
                {
                    Balance += 5.20;
                }
                else if (x == 3)
                {
                    Balance += 10.00;
                }
                else if (x == 4)
                {
                    Balance += 12.50;
                }
                else if (x == 5)
                {
                    Balance += 3.50;
                }
                else if (x == 6)
                {
                    Balance += 8.00;
                }
                else if (x == 7)
                {
                    Balance += 5.00;
                }
                else if (x == 8)
                {
                    Balance += 5.00;
                }
                else if (x == 9)
                {
                    Balance += 6.00;
                }
                else if (x == 10)
                {
                    Balance += 7.90;
                }
                else if (x == 11)
                {
                    Balance = Balance + 3.00;
                    Console.WriteLine($"The table service is 3.00 \u20ac. The final Balance is: {Balance}");
                    break;
                }
                else
                {
                    Console.WriteLine("Error! The typed value is incorrect");
                }

                Console.WriteLine($"Current Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a number.");
            }
        }
    }
}

