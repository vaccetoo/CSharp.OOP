using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        private const double HenWeightMultiplaier = 0.35;


        public Hen(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        public override IReadOnlyCollection<Type> FavouriteFood 
            => new List<Type>() { typeof(Meat), typeof(Vegetable), typeof(Fruit), typeof(Seeds) };

        protected override double WeightMultiplaier 
            => HenWeightMultiplaier;

        public override string ProduceSound() => "Cluck";
        
    }
}
