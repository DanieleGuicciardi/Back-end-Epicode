namespace L3_1202;

public class AverageAndSum
{
    public void function()
    {
        Console.Write("Enter the size of the array: ");
        
        if (int.TryParse(Console.ReadLine(), out int size) && size > 0)
        {
            int[] numbers = new int[size];
            int sum = 0;

            for (int i = 0; i < size; i++)
            {
                Console.Write($"Enter number {i + 1}: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    numbers[i] = number;
                    sum += number; 
                }
                else
                {
                    Console.WriteLine("Invalid number, please try again.");
                    i--; 
                }
            }

            double average = (double)sum / size; 

            Console.WriteLine($"\nTotal sum: {sum}");
            Console.WriteLine($"Arithmetic mean: {average:F2}");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a positive integer.");
        }
    }
    
}