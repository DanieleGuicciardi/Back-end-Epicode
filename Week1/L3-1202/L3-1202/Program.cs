using System;

class Program
{
    static void Main()
    {
        Console.Write("Inserisci il nome dell'account: ");
        string accountName = Console.ReadLine();
        BankAccount account = new BankAccount(accountName);
        account.Menu();
    }
}