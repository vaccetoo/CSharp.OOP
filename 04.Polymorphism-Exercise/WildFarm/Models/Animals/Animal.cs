using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animals
{
    public abstract class Animal : IAnimal
    {
        public Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }


        public string Name { get; private set; }

        public double Weight { get; private set; }

        public int FoodEaten { get; private set; }

        public abstract IReadOnlyCollection<Type> FavouriteFood { get; }

        protected abstract double WeightMultiplaier { get; }

        public void Eat(IFood food)
        {
            if (FavouriteFood.Any(f => f.Name == food.GetType().Name))
            {
                Weight += food.Quantity * WeightMultiplaier;

                FoodEaten += food.Quantity;
            }
            else
            {
                throw new ArgumentException($"{GetType().Name} does not eat {food.GetType().Name}!");
            }
        }

        public abstract string ProduceSound();
    }
}
