using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxygenLevel; // sec
        private List<string> caughtFish;
        private double cometitionPoints = 0;
        private bool hasHealthIssue = false;

        protected Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;

            caughtFish = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DiversNameNull);
                }
                
                name = value;
            }
        }

        public int OxygenLevel
        {
            get => oxygenLevel;
            protected set
            {
                if (value < 0)
                {
                    oxygenLevel = 0;
                }

                oxygenLevel = value;
            }
        }


        public IReadOnlyCollection<string> Catch
            => caughtFish;

        public double CompetitionPoints
            => cometitionPoints;

        public virtual bool HasHealthIssues 
            => hasHealthIssue;

        public void Hit(IFish fish)
        {
            int reducedOxygen = fish.TimeToCatch;
            OxygenLevel -= reducedOxygen;

            caughtFish.Add(fish.Name);

            cometitionPoints += fish.Points;
        }

        public abstract void Miss(int TimeToCatch);

        public abstract void RenewOxy();

        public void UpdateHealthStatus()
        {
            if (!hasHealthIssue)
            {
                hasHealthIssue = true;
            }
            else
            {
                hasHealthIssue = false;
            }
        }


        public override string ToString()
            => $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {caughtFish.Count}, Points earned: {CompetitionPoints} ]";
    }
}
