using BorderControl.Core.Interfaces;
using BorderControl.Models;
using BorderControl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            List<IBirtheble> peopleAndPets = new List<IBirtheble>();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                string[] commandInfo = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                IBirtheble birthable;

                if (commandInfo[0] == "Citizen")
                {
                    string name = commandInfo[1];
                    int age = int.Parse(commandInfo[2]);
                    string id = commandInfo[3];
                    string birthday = commandInfo[4];

                    birthable = new Person(name, age, id, birthday);
                }
                else if (commandInfo[0] == "Pet")
                {
                    string name = commandInfo[1];
                    string birthday = commandInfo[2];

                    birthable = new Pet(name, birthday);
                }
                else
                {
                    continue;
                }

                peopleAndPets.Add(birthable);   
            }

            string year = Console.ReadLine();

            foreach (var item in peopleAndPets)
            {
                if (item.Birthday.EndsWith(year))
                {
                    Console.WriteLine(item.Birthday);
                }
            }
        }
    }
}
