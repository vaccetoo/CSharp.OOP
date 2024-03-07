using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Dog : Mammal
    {
        private const double DogWeightMultiplaier = 0.40;


        public Dog(string name, double weight, string livingRegion) 
            : base(name, weight, livingRegion)
        {
        }

        public override IReadOnlyCollection<Type> FavouriteFood 
            => new List<Type>() { typeof(Meat) };

        protected override double WeightMultiplaier 
            => DogWeightMultiplaier;

        public override string ProduceSound() => "Woof!";
    }
}
