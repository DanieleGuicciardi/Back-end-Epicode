using System;

class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }

    public Person(string name, string surname, int age)
    {
        Name = name;
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

class Program
{
    static void Main()
    {
        Person person = new Person("Daniele", "Guicciardi", 21);
        
       Console.WriteLine("Here is the name of the person:");
       Console.WriteLine($"Name: {person.GetName()}");
       Console.WriteLine($"Surname: {person.GetSurname()}");
       Console.WriteLine($"Age: {person.GetAge()}");
       Console.WriteLine($"Here are the complete information: {person.GetInfo()}");
    }
}