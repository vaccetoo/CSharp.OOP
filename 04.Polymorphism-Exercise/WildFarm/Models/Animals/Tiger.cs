using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Tiger : Feline
    {
        private const double TigerWeightMultiplaier = 1.0;


        public Tiger(string name, double weight, string livingRegion, string breed) 
            : base(name, weight, livingRegion, breed)
        {
        }

        public override IReadOnlyCollection<Type> FavouriteFood
            => new List<Type>() { typeof(Meat) };

        protected override double WeightMultiplaier 
            => TigerWeightMultiplaier;

        public override string ProduceSound() => "ROAR!!!";
    }
}
