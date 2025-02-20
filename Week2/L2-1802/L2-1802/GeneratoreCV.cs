public class GeneratoreCV
{

    public string CheckPhone()
    {
        Console.WriteLine("Inserisci il tuo numero di telefono: ");
        string checkCellulare = Console.ReadLine();
    
        if (checkCellulare.Length >= 7 && checkCellulare.Length <= 15 && checkCellulare.All(char.IsDigit))
        {
            return checkCellulare;
        }
        else
        {
            Console.WriteLine("Il numero scelto non è valido!");
            return CheckPhone(); // Chiedi di nuovo un numero valido
        }
    }
    
    
    public void Function()
    {
        Console.WriteLine("Per iniziare a creare il tuo curriculm premi Enter!");
        Console.ReadLine();
        
        //informazioni personali
        InformazioniPersonali persona = new InformazioniPersonali();
        
        Console.WriteLine("Inserisci il tuo Nome: ");
        string Nome = Console.ReadLine();
        Console.WriteLine("Inserisci il tuo cognome: ");
        string Cognome = Console.ReadLine();
        
        persona.Cellulare = CheckPhone();
        
        Console.WriteLine("Inserisci il tuo indirizzo Mail: ");
        string Mail = Console.ReadLine();
        
        //studi
        Console.WriteLine("Quanti titoli di studio hai? ");
        int x = int.Parse(Console.ReadLine());

        Studi studi = new Studi();
        
        for (int i = 0; i < x; i++)
        {
            Console.WriteLine("Inserisci la qualifica: ");
            string Qualifica = Console.ReadLine();
            Console.WriteLine("Inserisci l istituto che ha emesso la qualifica: ");
            string Istituto = Console.ReadLine();
            Console.WriteLine("Inserisci il tipo");
            string Tipo = Console.ReadLine();
            Console.WriteLine("Inserisci la data di inizio studi nel formato aaaa-mm-gg");
            string Dal = Console.ReadLine();
            Console.WriteLine("Inserisci la data di fine studi nel formato aaaa-mm-gg");
            string Al = Console.ReadLine();
        }
        
        //esperienze
        Console.WriteLine("Quanti lavori hai fatto?");
        int x = int.Parse(Console.ReadLine());
        
        Impiego impiego = new Impiego();

        for (int i = 0; i < x; i++)
        {
            Console.WriteLine("Inserisci l Azienda: ");
            string Azienda = Console.ReadLine();
            Console.WriteLine("Inserisci l impiego che hai ricoperto: ");
            string JobTitle = Console.ReadLine();
            Console.WriteLine("Inserisci la data di inizio nel formato aaaa-mm-gg");
            string Dal = Console.ReadLine();
            Console.WriteLine("Inserisci la data di fine nel formato aaaa-mm-gg");
            string Al = Console.ReadLine();
            Console.WriteLine("Fornisci una breve descrizione del tuo ruolo");
            string Descrizione = Console.ReadLine();
        }
        
        
    }    
}