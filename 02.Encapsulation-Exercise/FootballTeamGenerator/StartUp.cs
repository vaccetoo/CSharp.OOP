
using FootballTeamGenerator.Models;
using System.Reflection.Metadata.Ecma335;

namespace FootballTeamGenerator;

public class StartUp
{
    public static void Main()
    {
        string command = string.Empty;

        List<Team> teams = new List<Team>();

        while ((command = Console.ReadLine()) != "END")
        {
            try
            {
                string[] commandInfo = command.Split(';');

                if (command.StartsWith("Team"))
                {
                    string teamName = commandInfo[1];

                    Team team = new(teamName);
                    teams.Add(team);
                }
                else if (command.StartsWith("Add"))
                {
                    // If current team exist
                    string teamName = commandInfo[1];
                    // Create player and add it to the team
                    string playerName = commandInfo[2];
                    int endurance = int.Parse(commandInfo[3]);
                    int sprint = int.Parse(commandInfo[4]);
                    int dribble = int.Parse(commandInfo[5]);
                    int passing = int.Parse(commandInfo[6]);
                    int shooting = int.Parse(commandInfo[7]);

                    if (teams.Any(n => n.Name == teamName))
                    {
                        Player player = new(playerName, endurance, sprint, dribble, passing, shooting);

                        foreach (Team team in teams)
                        {
                            if (team.Name == teamName)
                            {
                                team.AddPlayer(player);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                }
                else if (command.StartsWith("Remove"))
                {
                    string teamName = commandInfo[1];
                    string playerName = commandInfo[2];

                    foreach (Team team in teams)
                    {
                        if (team.Name == teamName)
                        {
                            if (team.Players.Any(n => n.Name == playerName))
                            {
                                team.RemovePlayer(playerName);
                            }
                            else
                            {
                                Console.WriteLine($"Player {playerName} is not in {teamName} team.");
                            }
                        }
                    }
                }
                else if (command.StartsWith("Rating"))
                {
                    string teamName = commandInfo[1];

                    if (teams.Any(n => n.Name == teamName))
                    {
                        foreach (var team in teams.Where(n => n.Name == teamName))
                        {
                            Console.WriteLine(team);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

    }
}
