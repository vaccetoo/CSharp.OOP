﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Owl : Bird
    {
        private const double OwlWeightMultiplaier = 0.25;


        public Owl(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        public override IReadOnlyCollection<Type> FavouriteFood
             => new List<Type>() { typeof(Meat) };

        protected override double WeightMultiplaier 
            => OwlWeightMultiplaier;

        public override string ProduceSound() => "Hoot Hoot";
    }
}
