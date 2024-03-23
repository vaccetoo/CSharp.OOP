using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Repositories
{
    public class FishRepository : IRepository<IFish>
    {
        private List<IFish> allFish;


        public FishRepository()
        {
            allFish = new List<IFish>();    
        }


        public IReadOnlyCollection<IFish> Models 
            => allFish;

        public void AddModel(IFish model)
        {
            allFish.Add(model);
        }

        public IFish GetModel(string name)
            => allFish.FirstOrDefault(f => f.Name == name);
    }
}
