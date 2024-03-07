using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        private const double MouseWeightMultiplaier = 0.10;


        public Mouse(string name, double weight, string livingRegion) 
            : base(name, weight, livingRegion)
        {
        }

        public override IReadOnlyCollection<Type> FavouriteFood
             => new List<Type>() { typeof(Vegetable), typeof(Fruit) };

        protected override double WeightMultiplaier 
            => MouseWeightMultiplaier;

        public override string ProduceSound() => "Squeak";
    }
}
