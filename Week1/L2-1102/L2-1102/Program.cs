using System;

class Person
{
    //dichiarazine delle 3 variabili
    public string Name { get; set; }  //permettono di settera e prendere la variabile
    public string Surname { get; set; }
    public int Age { get; set; }
    
    public Person(string name, string surname, int age) //COSTRUTTORE, riceve 3 parametri
    {
        Name = name;      //assegna ai parametri le proprieta
        Surname = surname;
        Age = age;
    }

    public string GetName()
    {
        return Name;
    }
    public string GetSurname()
    {
        return Surname;
    }
    public int GetAge()
    {
        return Age;
    }

    public string GetInfo()
    {
        return $"Name: {Name} | Surname: {Surname} | Age: {Age}";
    }
    
}

class Program   //questa classe deve essere sempre presente per far funzionare il programma
{              
    static void Main()
    {
        //qui viene richiamato il COSTRUTTORE, quindi assegna i valori ai parametri
        Person person = new Person("Daniele", "Guicciardi", 21);
        
       Console.WriteLine("Here is the name of the person:");
       Console.WriteLine($"Name: {person.GetName()}");
       Console.WriteLine($"Surname: {person.GetSurname()}");
       Console.WriteLine($"Age: {person.GetAge()}");
       Console.WriteLine($"Here are the complete information: {person.GetInfo()}");
    }
}