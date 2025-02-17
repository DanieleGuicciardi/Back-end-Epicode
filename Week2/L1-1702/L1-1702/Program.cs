using System;

class Program
{
    static void Main()
    {
        Console.Write("Welcome to the restaurant app, press Enter to see the menu!");
        Console.ReadLine();
        
        Menu menu = new Menu();
        menu.PrintMenu();

        Calculator calculator = new Calculator();
        calculator.Function();
    }
}