using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;
        private int pointsEarned;
        private List<IPlayer> teamPlayers;

        public Team(string name)
        {
            Name = name;

            teamPlayers = new List<IPlayer>();
            pointsEarned = 0;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            }
        }

        public int PointsEarned
            => pointsEarned;

        public double OverallRating 
        { 
            get
            {
                if (!teamPlayers.Any())
                {
                    return 0;
                }

                double sumRatings = 0;

                foreach (var player in teamPlayers)
                {
                    sumRatings += player.Rating;
                }

                double avergeSum = sumRatings / teamPlayers.Count;

                return Math.Round(avergeSum, 2);
            }
        }

        public IReadOnlyCollection<IPlayer> Players
            => teamPlayers;

        public void Draw()
        {
            pointsEarned += 1;

            IPlayer goalkeeper = teamPlayers.First(p => p.GetType().Name == "Goalkeeper");
            goalkeeper.IncreaseRating();
        }

        public void Lose()
        {
            foreach (IPlayer player in teamPlayers)
            {
                player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player)
        {
            teamPlayers.Add(player);
        }

        public void Win()
        {
            pointsEarned += 3;

            foreach (IPlayer player in teamPlayers)
            {
                player.IncreaseRating();
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");
            sb.Append("--Players: ");

            if (teamPlayers.Any())
            {
                foreach (IPlayer player in teamPlayers)
                {
                    sb.Append($"{player.Name}, ");
                }
            }
            else
            {
                sb.Append("none");
            }

            return sb.ToString().TrimEnd(',', ' ');
        }
    }
}
