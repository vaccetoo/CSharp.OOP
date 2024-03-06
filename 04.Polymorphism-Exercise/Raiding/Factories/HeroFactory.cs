using Raiding.Factories.Interfaces;
using Raiding.Models;
using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Factories
{
    public class HeroFactory : IHeroFactory
    {
        public IHero Create(string heroType, string name)
        {
            if (heroType == "Druid")
            {
                return new Druid(name);
            }
            else if (heroType == "Paladin")
            {
                return new Paladin(name);
            }
            else if(heroType == "Rogue")
            {
                return new Rogue(name);
            }
            else if (heroType == "Warrior")
            {
                return new Warrior(name);
            }
            else
            {
                throw new ArgumentException("Invalid hero!");
            }
        }
    }
}
