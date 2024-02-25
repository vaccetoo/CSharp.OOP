
namespace FootballTeamGenerator.Models
{
    public class Team
    {
        private string name;
        private List<Player> players;


        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }


        public string Name
        {
            get => name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"A name should not be empty.");
                }

                name = value;
            }
        }


        public IReadOnlyCollection<Player> Players => players;

        private double TeamRating
            => players.Sum(r => r.GetStats) / players.Count;

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(string player)
        {
            if (players.Any(n => n.Name == player))
            {
                players.RemoveAll(n => n.Name == player);
            }
        }

        public override string ToString()
        {
            if (double.IsNaN(TeamRating))
            {
                return $"{Name} - 0";
            }
            return $"{Name} - {Math.Round(TeamRating)}";
        }

    }
}
