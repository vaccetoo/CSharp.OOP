using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double CatWeightMultiplaier = 0.30;


        public Cat(string name, double weight, string livingRegion, string breed) 
            : base(name, weight, livingRegion, breed)
        {
        }

        public override IReadOnlyCollection<Type> FavouriteFood
             => new List<Type>() { typeof(Meat), typeof(Vegetable) };

        protected override double WeightMultiplaier 
            => CatWeightMultiplaier;

        public override string ProduceSound() => "Meow";
    }
}
