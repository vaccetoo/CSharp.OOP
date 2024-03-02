using FoodShortage.Core.Interfaces;
using FoodShortage.Models;
using FoodShortage.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShortage.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            int numberOfPeople = int.Parse(Console.ReadLine());

            List<IBuyer> customers = new List<IBuyer>();

            for (int i = 0; i < numberOfPeople; i++)
            {
                string[] personInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                IBuyer buyer;

                if (personInfo.Length == 4)
                {
                    string name = personInfo[0];
                    int age = int.Parse(personInfo[1]);
                    string id = personInfo[2];
                    string birthday = personInfo[3];

                    buyer = new Citizen(name, age, id, birthday);
                }
                else
                {
                    string name = personInfo[0];
                    int age = int.Parse(personInfo[1]);
                    string group = personInfo[2];

                    buyer = new Rebel(name, age, group);
                }

                customers.Add(buyer);
            }


            string command = string.Empty;


            while ((command = Console.ReadLine()) != "End")
            {
                customers.FirstOrDefault(c => c.Name == command)?.BuyFood();
            }

            Console.WriteLine(customers.Sum(c => c.Food));
        }
    }
}
