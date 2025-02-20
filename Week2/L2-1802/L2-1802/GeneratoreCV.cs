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
    
    private DateOnly LeggiData(string messaggio)
    {
        DateOnly data;
        while (true)
        {
            Console.WriteLine(messaggio);
            if (DateOnly.TryParse(Console.ReadLine(), out data))
                return data;
            Console.WriteLine("Formato data non valido, riprova.");
        }
    }
    
        public void Function()
    {
        Console.WriteLine("Per iniziare a creare il tuo curriculum premi Enter!");
        Console.ReadLine();

        // Informazioni personali
        InformazioniPersonali persona = new InformazioniPersonali();

        Console.WriteLine("Inserisci il tuo Nome: ");
        persona.Nome = Console.ReadLine();
        Console.WriteLine("Inserisci il tuo Cognome: ");
        persona.Cognome = Console.ReadLine();
        persona.Cellulare = CheckPhone();
        Console.WriteLine("Inserisci il tuo indirizzo Mail: ");
        persona.Mail = Console.ReadLine();

        // Studi
        Console.WriteLine("Quanti titoli di studio hai?");
        int numStudi;
        while (!int.TryParse(Console.ReadLine(), out numStudi) || numStudi < 0)
        {
            Console.WriteLine("Inserisci un numero valido!");
        }

        List<Studi> studiList = new List<Studi>();

        for (int i = 0; i < numStudi; i++)
        {
            Studi studi = new Studi();
            Console.WriteLine($"Inserisci la qualifica {i + 1}: ");
            studi.Qualifica = Console.ReadLine();
            Console.WriteLine("Inserisci l'istituto che ha emesso la qualifica: ");
            studi.Istituto = Console.ReadLine();
            Console.WriteLine("Inserisci il tipo di titolo di studio: ");
            studi.Tipo = Console.ReadLine();

            studi.Dal = LeggiData("Inserisci la data di inizio studi (aaaa-mm-gg): ");
            studi.Al = LeggiData("Inserisci la data di fine studi (aaaa-mm-gg): ");

            studiList.Add(studi);
        }

        // Esperienze lavorative
        Console.WriteLine("Quanti lavori hai fatto?");
        int numLavori;
        while (!int.TryParse(Console.ReadLine(), out numLavori) || numLavori < 0)
        {
            Console.WriteLine("Inserisci un numero valido!");
        }

        Impiego impiego = new Impiego();

        for (int i = 0; i < numLavori; i++)
        {
            Esperienza esperienza = new Esperienza();
            Console.WriteLine($"Inserisci il nome dell'Azienda {i + 1}: ");
            esperienza.Azienda = Console.ReadLine();
            Console.WriteLine("Inserisci il titolo dell'impiego ricoperto: ");
            esperienza.JobTitle = Console.ReadLine();

            esperienza.Dal = LeggiData("Inserisci la data di inizio impiego (aaaa-mm-gg): ");
            esperienza.Al = LeggiData("Inserisci la data di fine impiego (aaaa-mm-gg): ");

            Console.WriteLine("Fornisci una breve descrizione del tuo ruolo: ");
            esperienza.Descrizione = Console.ReadLine();

            impiego.Esperienze.Add(esperienza);
        }

        // Stampare il CV finale
        StampaCV(persona, studiList, impiego);
    }
        
    private void StampaCV(InformazioniPersonali persona, List<Studi> studiList, Impiego impiego)
    {
        Console.WriteLine("\n--------------------------------");
        Console.WriteLine("          CURRICULUM VITAE      ");
        Console.WriteLine("--------------------------------\n");

        // Stampa informazioni personali
        Console.WriteLine("Informazioni Personali:");
        Console.WriteLine($"Nome: {persona.Nome} {persona.Cognome}");
        Console.WriteLine($"Cellulare: {persona.Cellulare}");
        Console.WriteLine($"Email: {persona.Mail}");
        Console.WriteLine("--------------------------------\n");

        // Stampa studi
        Console.WriteLine("Studi:");
        if (studiList.Count > 0)
        {
            foreach (var studi in studiList)
            {
                Console.WriteLine($"- {studi.Qualifica} presso {studi.Istituto} ({studi.Dal} - {studi.Al})");
            }
        }
        else
        {
            Console.WriteLine("Nessun titolo di studio inserito.");
        }
        Console.WriteLine("--------------------------------\n");

        // Stampa esperienze lavorative
        Console.WriteLine("Esperienze Lavorative:");
        if (impiego.Esperienze.Count > 0)
        {
            foreach (var esperienza in impiego.Esperienze)
            {
                Console.WriteLine($"- {esperienza.JobTitle} presso {esperienza.Azienda} ({esperienza.Dal} - {esperienza.Al})");
                Console.WriteLine($"  {esperienza.Descrizione}");
            }
        }
        else
        {
            Console.WriteLine("Nessuna esperienza lavorativa inserita.");
        }
        Console.WriteLine("\n--------------------------------");
        Console.WriteLine("         FINE DEL CV            ");
        Console.WriteLine("--------------------------------\n");
    }
}