using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models.Contracts
{
    public abstract class Player : IPlayer
    {
        private string name;
        private double rating;
        private string team;

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.PlayerNameNull); 
                }
                name = value;
            }
        }

        public double Rating
        {
            get => rating;
            protected set
            {
                rating = value;
            }
        }

        public string Team 
        { 
            get => team;
            private set => team = value;
        }

        public abstract void DecreaseRating(); // min 1

        public abstract void IncreaseRating(); // max 10

        public void JoinTeam(string name)
        {
            Team = name;
        }
    }
}
