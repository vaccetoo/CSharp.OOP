
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


        public List<Player> Players { get => players; }

        private double TeamRating
            => players.Sum(r => r.GetStats) / players.Count;

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public bool RemovePlayer(Player player)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
                return true;
            }

            Console.WriteLine($"Player {player.Name} is not in {Name} team.");
            return false;
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
