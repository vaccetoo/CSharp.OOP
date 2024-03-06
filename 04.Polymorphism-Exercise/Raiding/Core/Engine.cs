using Raiding.Core.Interfaces;
using Raiding.Factories.Interfaces;
using Raiding.Models;
using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IHeroFactory heroFactory;
        private readonly ICollection<IHero> heroCollection;


        public Engine(IHeroFactory heroFactory)
        {
            this.heroFactory = heroFactory;
            heroCollection = new List<IHero>();
        }


        public void Run()
        {
            int neededHeroes = int.Parse(Console.ReadLine());

            while (neededHeroes > 0)
            {
                string heroName = Console.ReadLine();
                string heroType = Console.ReadLine();

                try
                {
                    IHero hero = heroFactory.Create(heroType, heroName);
                    heroCollection.Add(hero);
                    neededHeroes--;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            int bossPower = int.Parse(Console.ReadLine());

            foreach (IHero hero in heroCollection)
            {
                bossPower -= hero.Power;

                Console.WriteLine(hero.CastAbility());
            }

            if (bossPower <= 0)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}
