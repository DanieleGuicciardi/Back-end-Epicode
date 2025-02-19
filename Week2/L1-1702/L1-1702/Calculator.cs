using System;
using System.Collections.Generic;

public class Calculator
{
    private double Balance { get; set; }
    private List<string> selectedItems; // Lista per tenere traccia degli acquisti

    public Calculator()
    {
        Balance = 0;
        selectedItems = new List<string>(); // Inizializza la lista
    }

    public void Function()
    {
        while (true) // Loop infinito
        {
            Console.WriteLine("What would you like to eat? Type the number (or type 0 to exit): ");

            if (int.TryParse(Console.ReadLine(), out int x))
            {
                switch (x)
                {
                    case 1:
                        Balance += 2.50;
                        selectedItems.Add("Coca Cola 150 ml - €2.50");
                        break;
                    case 2:
                        Balance += 5.20;
                        selectedItems.Add("Insalata di pollo - €5.20");
                        break;
                    case 3:
                        Balance += 10.00;
                        selectedItems.Add("Pizza Margherita - €10.00");
                        break;
                    case 4:
                        Balance += 12.50;
                        selectedItems.Add("Pizza 4 Formaggi - €12.50");
                        break;
                    case 5:
                        Balance += 3.50;
                        selectedItems.Add("Patatine fritte - €3.50");
                        break;
                    case 6:
                        Balance += 8.00;
                        selectedItems.Add("Insalata di riso - €8.00");
                        break;
                    case 7:
                        Balance += 5.00;
                        selectedItems.Add("Frutta di stagione - €5.00");
                        break;
                    case 8:
                        Balance += 5.00;
                        selectedItems.Add("Pizza fritta - €5.00");
                        break;
                    case 9:
                        Balance += 6.00;
                        selectedItems.Add("Piadina vegetariana - €6.00");
                        break;
                    case 10:
                        Balance += 7.90;
                        selectedItems.Add("Panino Hamburger - €7.90");
                        break;
                    case 11:
                        Balance += 3.00;
                        selectedItems.Add("Servizio al tavolo - €3.00");

                        Console.WriteLine("\n============== CONTO FINALE ==============");
                        foreach (var item in selectedItems)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine($"------------------------------------------");
                        Console.WriteLine($"Totale da pagare: €{Balance:F2}");
                        Console.WriteLine("==========================================\n");
                        return; // Esce dal metodo e termina il programma
                    case 0:
                        Console.WriteLine("Uscita dal menu...");
                        return;
                    default:
                        Console.WriteLine("Errore! Il numero digitato non è valido.");
                        break;
                }

                Console.WriteLine($"Saldo attuale: €{Balance:F2}");
            }
            else
            {
                Console.WriteLine("Input non valido! Inserisci un numero.");
            }
        }
    }
}


