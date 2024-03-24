using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> playerRepository;
        private IRepository<ITeam> teamRepository;


        public Controller()
        {
            playerRepository = new PlayerRepository();
            teamRepository = new TeamRepository();
        }


        public string LeagueStandings()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***League Standings***");

            foreach (var team in teamRepository.Models
                .OrderByDescending(t => t.PointsEarned)
                .ThenByDescending(t => t.OverallRating)
                .ThenBy(t => t.Name))
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!playerRepository.ExistsModel(playerName))
            {
                return $"Player with the name {playerName} does not exist in the {typeof(PlayerRepository).Name}.";
            }

            if (!teamRepository.ExistsModel(teamName))
            {
                return $"Team with the name {teamName} does not exist in the {typeof(TeamRepository).Name}.";
            }

            if (playerRepository.GetModel(playerName).Team != null)
            {
                return $"Player {playerName} has already signed with {playerRepository.GetModel(playerName).Team}.";
            }

            playerRepository.GetModel(playerName).JoinTeam(teamName);

            teamRepository.GetModel(teamName)
                          .SignContract(playerRepository
                          .GetModel(playerName));

            return $"Player {playerName} signed a contract with {teamName}.";
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firstTeam = teamRepository.GetModel(firstTeamName);
            ITeam secondTeam = teamRepository.GetModel(secondTeamName);

            if (firstTeam.OverallRating == secondTeam.OverallRating)
            {
                firstTeam.Draw();
                secondTeam.Draw();

                return $"The game between {firstTeamName} and {secondTeamName} ends in a draw!";
            }
            else
            {
                if (firstTeam.OverallRating > secondTeam.OverallRating)
                {
                    firstTeam.Win();
                    secondTeam.Lose();

                    return $"Team {firstTeam.Name} wins the game over {secondTeam.Name}!";
                }
                else 
                {
                    secondTeam.Win();
                    firstTeam.Lose();

                    return $"Team {secondTeam.Name} wins the game over {firstTeam.Name}!";
                }
            }
        }

        public string NewPlayer(string typeName, string name)
        {
            Type type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == typeName);

            if (type == null)
            {
                return $"{typeName} is invalid position for the application.";
            }

            if (playerRepository.ExistsModel(name))
            {
                IPlayer player = playerRepository.GetModel(name);

                return $"{name} is already added to the {typeof(PlayerRepository).Name} as {player.GetType().Name}.";
            }

            IPlayer newPlayer = Activator.CreateInstance(type, name) as IPlayer;

            playerRepository.AddModel(newPlayer);

            return $"{name} is filed for the handball league.";
        }

        public string NewTeam(string name)
        {
            if (teamRepository.ExistsModel(name))
            {
                return $"{name} is already added to the {typeof(TeamRepository).Name}.";
            }

            ITeam team = new Team(name);
            teamRepository.AddModel(team);

            return $"{name} is successfully added to the {typeof(TeamRepository).Name}.";
        }

        public string PlayerStatistics(string teamName)
        {
            ITeam team = teamRepository.GetModel(teamName);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"***{team.Name}***");

            foreach (var player in team.Players.OrderByDescending(p => p.Rating).ThenBy(p => p.Name))
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();

        }
    }
}
