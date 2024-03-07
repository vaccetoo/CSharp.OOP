using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Core.Interfaces;
using WildFarm.Factories.Interfaces;
using WildFarm.Models.Animals;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private IAnimalFactory animalFactory;
        private IFoodFactory foodFactory;
        private ICollection<IAnimal> animals;


        public Engine(IAnimalFactory animalFactory, IFoodFactory foodFactory)
        {
            this.animalFactory = animalFactory;
            this.foodFactory = foodFactory;
            animals = new List<IAnimal>();
        }


        public void Run()
        {
            string command = string.Empty;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] foodInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string[] animalInfo = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    IAnimal animal = CreateAnimal(animalInfo);
                    IFood food = CreateFood(foodInfo);

                    animals.Add(animal);

                    Console.WriteLine(animal.ProduceSound());
                    animal.Eat(food);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }



        private IFood CreateFood(string[] foodInfo)
        {
            string foodType = foodInfo[0];
            int foodQuantity = int.Parse(foodInfo[1]);

            return foodFactory.CreateFood(foodType, foodQuantity);
        }


        private IAnimal CreateAnimal(string[] animalInfo)
            => animalFactory.CreateAnimal(animalInfo);
    }
}
