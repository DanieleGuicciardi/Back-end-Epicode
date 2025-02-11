using System;

class Program
{
    static void Main()
    {
        Atlete atlete = new Atlete("Giorgio Palazzo", "Runner", "M");
        Employee employee = new Employee(12, "Developer");
        Animal animal = new Animal("Dog");
        Car car = new Car("Alfa Romeo", "MITO");

        atlete.PrintInfo();
        employee.PrintInfo();
        animal.PrintInfo();
        car.PrintInfo();
    }
}