using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Factories.Interfaces;
using WildFarm.Models.Animals;
using WildFarm.Models.Interfaces;

namespace WildFarm.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        public IAnimal CreateAnimal(string[] animalInfo)
        {
            string animalType = animalInfo[0];
            string name = animalInfo[1];
            double weight = double.Parse(animalInfo[2]);

            if (animalType == "Owl")
            {
                return new Owl(name, weight, double.Parse(animalInfo[3]));
            }
            else if (animalType == "Hen")
            {
                return new Hen(name, weight, double.Parse(animalInfo[3]));
            }
            else if (animalType == "Mouse")
            {
                return new Mouse(name, weight, animalInfo[3]);
            }
            else if (animalType == "Dog")
            {
                return new Dog(name, weight, animalInfo[3]);
            }
            else if (animalType == "Cat")
            {
                return new Cat(name, weight, animalInfo[3], animalInfo[4]);
            }
            else if (animalType == "Tiger")
            {
                return new Tiger(name, weight, animalInfo[3], animalInfo[4]);
            }
            else
            {
                throw new ArgumentException("Invalid Animal type");
            }
        }
    }
}
