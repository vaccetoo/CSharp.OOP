using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
{
    public class AnonymousImpactUnit : MilitaryUnit
    {
        private const double InitialPrice = 30.0;

        public AnonymousImpactUnit() 
            : base(InitialPrice)
        {
        }
    }
}
