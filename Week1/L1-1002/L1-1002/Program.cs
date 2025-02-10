using System;

public class Dipendente
{
    public string Nome;
    public string Ruolo;

    public Dipendente(string nome, string ruolo)
    {
        Nome = nome;
        Ruolo = ruolo;
    }

    public void Presentati()
    {
        Console.WriteLine($"Ciao, mi chiamo {Nome} e sono un {Ruolo}.");
    }
}

public class Animale
{
    public string Nome;
    public string Verso;

    public Animale(string nome, string verso)
    {
        Nome = nome;
        Verso = verso;
    }

    public void FaiVerso()
    {
        Console.WriteLine($"Il {Nome}, {Verso}!");
    }
}

public class Veicolo
{
    public string Marca;
    public int VelocitaMassima;

    public Veicolo(string marca, int velocita)
    {
        Marca = marca;
        VelocitaMassima = velocita;
    }

    public void MostraInfo()
    {
        Console.WriteLine($"Questa {Marca} va fino ai {VelocitaMassima} km/h!");
    }
}

public class Atleta
{
    public int Age;

    public Atleta(int age)
    {
        Age = age;
    }

    public void MostraEta()
    {
        Console.WriteLine($"Sono un atleta e ho {Age} anni.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Dipendente dip = new Dipendente("Daniele", "programmatore");
        dip.Presentati();

        Animale cane = new Animale("cane", "abbaia");
        cane.FaiVerso();

        Veicolo auto = new Veicolo("ferrari", 300);
        auto.MostraInfo();

        Atleta atleta = new Atleta(25);
        atleta.MostraEta();
    }
}

