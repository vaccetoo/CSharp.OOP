
namespace AnimalFarm;

using System;
using AnimalFarm.Models;
public class StartUp
{
    static void Main(string[] args)
    {
        string name = Console.ReadLine();
        int age = int.Parse(Console.ReadLine());

        try
        {
            Chicken chicken = new Chicken(name, age);
            Console.WriteLine(chicken.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
