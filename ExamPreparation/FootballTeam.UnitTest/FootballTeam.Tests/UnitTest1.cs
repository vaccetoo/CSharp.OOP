using NUnit.Framework;
using System;
using System.Xml.Linq;

namespace FootballTeam.Tests
{
    public class Tests
    {
        [TestCase(null)]
        [TestCase("")]
        public void NameIsNullOeEmpty(string name)
        {
            Assert.Throws<ArgumentException>(() => new FootballTeam(name, 15), "Name cannot be null or empty!");
        }

        [TestCase("Team1")]
        [TestCase("Liverpool")]
        [TestCase("Loko pd")]
        public void NameWorksCorrect(string name)
        {
            FootballTeam footballTeam = new FootballTeam(name, 15);

            Assert.AreEqual(name, footballTeam.Name);
        }

        [TestCase(14)]
        [TestCase(-1)]
        [TestCase(0)]
        public void CapacityLessThan15(int players)
        {
            Assert.Throws<ArgumentException>(() => new FootballTeam("Team", players), "Capacity min value = 15");
        }

        [TestCase(15)]
        [TestCase(2000)]
        public void CapacityWorksCorrect(int players)
        {
            FootballTeam footballTeam = new FootballTeam("Team", players);

            Assert.AreEqual(players, footballTeam.Capacity);
        }

        [TestCase(15)]
        public void AddNewPlayerNoMoreCapacity(int players)
        {
            FootballTeam footballTeam = new FootballTeam("Team", players);

            FootballPlayer player1 = new FootballPlayer("Name1", 1, "Midfielder");
            FootballPlayer player2 = new FootballPlayer("Name2", 2, "Midfielder");
            FootballPlayer player3 = new FootballPlayer("Name1", 3, "Midfielder");
            FootballPlayer player4 = new FootballPlayer("Name1", 4, "Midfielder");
            FootballPlayer player5 = new FootballPlayer("Name1", 5, "Midfielder");
            FootballPlayer player6 = new FootballPlayer("Name1", 6, "Midfielder");
            FootballPlayer player7 = new FootballPlayer("Name1", 7, "Midfielder");
            FootballPlayer player8 = new FootballPlayer("Name1", 8, "Midfielder");
            FootballPlayer player9 = new FootballPlayer("Name1", 9, "Midfielder");
            FootballPlayer player10 = new FootballPlayer("Name1", 10, "Midfielder");
            FootballPlayer player11 = new FootballPlayer("Name1", 11, "Midfielder");
            FootballPlayer player12 = new FootballPlayer("Name1", 12, "Midfielder");
            FootballPlayer player13 = new FootballPlayer("Name1", 13, "Midfielder");
            FootballPlayer player14 = new FootballPlayer("Name1", 14, "Midfielder");
            FootballPlayer player15 = new FootballPlayer("Name1", 15, "Midfielder");

            FootballPlayer player16 = new FootballPlayer("Name1", 16, "Midfielder");

            footballTeam.AddNewPlayer(player1);
            footballTeam.AddNewPlayer(player2);
            footballTeam.AddNewPlayer(player3);
            footballTeam.AddNewPlayer(player4);
            footballTeam.AddNewPlayer(player5);
            footballTeam.AddNewPlayer(player6);
            footballTeam.AddNewPlayer(player7);
            footballTeam.AddNewPlayer(player8);
            footballTeam.AddNewPlayer(player9);
            footballTeam.AddNewPlayer(player10);
            footballTeam.AddNewPlayer(player11);
            footballTeam.AddNewPlayer(player12);
            footballTeam.AddNewPlayer(player13);
            footballTeam.AddNewPlayer(player14);
            footballTeam.AddNewPlayer(player15);

            Assert.AreEqual("No more positions available!", footballTeam.AddNewPlayer(player16));

        }

        [TestCase(15)]
        public void AddNewPlayerWorksCorrect(int players)
        {
            FootballTeam footballTeam = new FootballTeam("Team", players);

            FootballPlayer player1 = new FootballPlayer("Name1", 1, "Midfielder");
            FootballPlayer player2 = new FootballPlayer("Name2", 2, "Midfielder");
            FootballPlayer player3 = new FootballPlayer("Name3", 3, "Midfielder");
            FootballPlayer player4 = new FootballPlayer("Name4", 4, "Midfielder");
            FootballPlayer player5 = new FootballPlayer("Name5", 5, "Midfielder");
            FootballPlayer player6 = new FootballPlayer("Name6", 6, "Midfielder");
            FootballPlayer player7 = new FootballPlayer("Name1", 7, "Midfielder");
            FootballPlayer player8 = new FootballPlayer("Name1", 8, "Midfielder");
            FootballPlayer player9 = new FootballPlayer("Name1", 9, "Midfielder");
            FootballPlayer player10 = new FootballPlayer("Name1", 10, "Midfielder");
            FootballPlayer player11 = new FootballPlayer("Name1", 11, "Midfielder");
            FootballPlayer player12 = new FootballPlayer("Name1", 12, "Midfielder");
            FootballPlayer player13 = new FootballPlayer("Name1", 13, "Midfielder");
            FootballPlayer player14 = new FootballPlayer("Name1", 14, "Midfielder");
            FootballPlayer player15 = new FootballPlayer("Name1", 15, "Midfielder");

            footballTeam.AddNewPlayer(player1);
            footballTeam.AddNewPlayer(player2);
            footballTeam.AddNewPlayer(player3);
            footballTeam.AddNewPlayer(player4);
            footballTeam.AddNewPlayer(player5);
            footballTeam.AddNewPlayer(player6);
            footballTeam.AddNewPlayer(player7);
            footballTeam.AddNewPlayer(player8);
            footballTeam.AddNewPlayer(player9);
            footballTeam.AddNewPlayer(player10);
            footballTeam.AddNewPlayer(player11);
            footballTeam.AddNewPlayer(player12);
            footballTeam.AddNewPlayer(player13);
            footballTeam.AddNewPlayer(player14);

            Assert.AreEqual("Added player Name1 in position Midfielder with number 15", footballTeam.AddNewPlayer(player15));
        }

        [TestCase(15)]
        public void PickPlayerReturnsCorrectPlayer(int players)
        {
            FootballTeam footballTeam = new FootballTeam("Team", players);

            FootballPlayer player1 = new FootballPlayer("Name1", 1, "Midfielder");
            FootballPlayer player2 = new FootballPlayer("Name2", 2, "Midfielder");
            FootballPlayer player3 = new FootballPlayer("Name3", 3, "Midfielder");
            FootballPlayer player4 = new FootballPlayer("Name4", 4, "Midfielder");
            FootballPlayer player5 = new FootballPlayer("Name5", 5, "Midfielder");
            FootballPlayer player6 = new FootballPlayer("Name6", 6, "Midfielder");
            FootballPlayer player7 = new FootballPlayer("Name7", 7, "Midfielder");
            FootballPlayer player8 = new FootballPlayer("Name8", 8, "Midfielder");
            FootballPlayer player9 = new FootballPlayer("Name9", 9, "Midfielder");
            FootballPlayer player10 = new FootballPlayer("Name10", 10, "Midfielder");
            FootballPlayer player11 = new FootballPlayer("Name11", 11, "Midfielder");
            FootballPlayer player12 = new FootballPlayer("Name12", 12, "Midfielder");
            FootballPlayer player13 = new FootballPlayer("Name13", 13, "Midfielder");
            FootballPlayer player14 = new FootballPlayer("Name14", 14, "Midfielder");
            FootballPlayer player15 = new FootballPlayer("Name15", 15, "Midfielder");

            footballTeam.AddNewPlayer(player1);
            footballTeam.AddNewPlayer(player2);
            footballTeam.AddNewPlayer(player3);
            footballTeam.AddNewPlayer(player4);
            footballTeam.AddNewPlayer(player5);
            footballTeam.AddNewPlayer(player6);
            footballTeam.AddNewPlayer(player7);
            footballTeam.AddNewPlayer(player8);
            footballTeam.AddNewPlayer(player9);
            footballTeam.AddNewPlayer(player10);
            footballTeam.AddNewPlayer(player11);
            footballTeam.AddNewPlayer(player12);
            footballTeam.AddNewPlayer(player13);
            footballTeam.AddNewPlayer(player14);
            footballTeam.AddNewPlayer(player15);

            Assert.AreEqual(player15, footballTeam.PickPlayer("Name15"));
        }

        [TestCase(15)]
        public void PickPlayerReturnsNull(int players)
        {
            FootballTeam footballTeam = new FootballTeam("Team", players);

            FootballPlayer player1 = new FootballPlayer("Name1", 1, "Midfielder");
            FootballPlayer player2 = new FootballPlayer("Name2", 2, "Midfielder");
            FootballPlayer player3 = new FootballPlayer("Name3", 3, "Midfielder");
            FootballPlayer player4 = new FootballPlayer("Name4", 4, "Midfielder");
            FootballPlayer player5 = new FootballPlayer("Name5", 5, "Midfielder");
            FootballPlayer player6 = new FootballPlayer("Name6", 6, "Midfielder");
            FootballPlayer player7 = new FootballPlayer("Name7", 7, "Midfielder");
            FootballPlayer player8 = new FootballPlayer("Name8", 8, "Midfielder");
            FootballPlayer player9 = new FootballPlayer("Name9", 9, "Midfielder");
            FootballPlayer player10 = new FootballPlayer("Name10", 10, "Midfielder");
            FootballPlayer player11 = new FootballPlayer("Name11", 11, "Midfielder");
            FootballPlayer player12 = new FootballPlayer("Name12", 12, "Midfielder");
            FootballPlayer player13 = new FootballPlayer("Name13", 13, "Midfielder");
            FootballPlayer player14 = new FootballPlayer("Name14", 14, "Midfielder");
            FootballPlayer player15 = new FootballPlayer("Name15", 15, "Midfielder");

            footballTeam.AddNewPlayer(player1);
            footballTeam.AddNewPlayer(player2);
            footballTeam.AddNewPlayer(player3);
            footballTeam.AddNewPlayer(player4);
            footballTeam.AddNewPlayer(player5);
            footballTeam.AddNewPlayer(player6);
            footballTeam.AddNewPlayer(player7);
            footballTeam.AddNewPlayer(player8);
            footballTeam.AddNewPlayer(player9);
            footballTeam.AddNewPlayer(player10);
            footballTeam.AddNewPlayer(player11);
            footballTeam.AddNewPlayer(player12);
            footballTeam.AddNewPlayer(player13);
            footballTeam.AddNewPlayer(player14);
            footballTeam.AddNewPlayer(player15);

            Assert.AreEqual(null, footballTeam.PickPlayer("Name16"));
        }

        [TestCase(15)]
        public void PlayerScoreIncresePlayerGoals(int players)
        {
            FootballTeam footballTeam = new FootballTeam("Team", players);

            FootballPlayer player1 = new FootballPlayer("Name1", 1, "Midfielder");
            FootballPlayer player2 = new FootballPlayer("Name2", 2, "Midfielder");
            FootballPlayer player3 = new FootballPlayer("Name3", 3, "Midfielder");
            FootballPlayer player4 = new FootballPlayer("Name4", 4, "Midfielder");
            FootballPlayer player5 = new FootballPlayer("Name5", 5, "Midfielder");
            FootballPlayer player6 = new FootballPlayer("Name6", 6, "Midfielder");
            FootballPlayer player7 = new FootballPlayer("Name7", 7, "Midfielder");
            FootballPlayer player8 = new FootballPlayer("Name8", 8, "Midfielder");
            FootballPlayer player9 = new FootballPlayer("Name9", 9, "Midfielder");
            FootballPlayer player10 = new FootballPlayer("Name10", 10, "Midfielder");
            FootballPlayer player11 = new FootballPlayer("Name11", 11, "Midfielder");
            FootballPlayer player12 = new FootballPlayer("Name12", 12, "Midfielder");
            FootballPlayer player13 = new FootballPlayer("Name13", 13, "Midfielder");
            FootballPlayer player14 = new FootballPlayer("Name14", 14, "Midfielder");
            FootballPlayer player15 = new FootballPlayer("Name15", 15, "Midfielder");

            footballTeam.AddNewPlayer(player1);
            footballTeam.AddNewPlayer(player2);
            footballTeam.AddNewPlayer(player3);
            footballTeam.AddNewPlayer(player4);
            footballTeam.AddNewPlayer(player5);
            footballTeam.AddNewPlayer(player6);
            footballTeam.AddNewPlayer(player7);
            footballTeam.AddNewPlayer(player8);
            footballTeam.AddNewPlayer(player9);
            footballTeam.AddNewPlayer(player10);
            footballTeam.AddNewPlayer(player11);
            footballTeam.AddNewPlayer(player12);
            footballTeam.AddNewPlayer(player13);
            footballTeam.AddNewPlayer(player14);
            footballTeam.AddNewPlayer(player15);

            footballTeam.PlayerScore(1);

            Assert.AreEqual(1, player1.ScoredGoals);
            Assert.AreEqual("Name1 scored and now has 2 for this season!", footballTeam.PlayerScore(1));
        }
    }
}