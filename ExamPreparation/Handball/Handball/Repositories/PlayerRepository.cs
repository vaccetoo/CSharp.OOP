using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private List<IPlayer> players;


        public PlayerRepository()
        {
            players = new List<IPlayer>();
        }


        public IReadOnlyCollection<IPlayer> Models 
            => players;

        public void AddModel(IPlayer model)
        {
            players.Add(model);
        }

        public bool ExistsModel(string name)
        {
            if (players.Any(p => p.Name == name))
            {
                return true;
            }

            return false;
        }

        public IPlayer GetModel(string name)
            => players.FirstOrDefault(p => p.Name == name); 

        public bool RemoveModel(string name)
        {
            if (players.Any(p => p.Name == name))
            {
                IPlayer player = players.First(p => p.Name == name);
                players.Remove(player);

                return true;
            }

            return false;
        }
    }
}
