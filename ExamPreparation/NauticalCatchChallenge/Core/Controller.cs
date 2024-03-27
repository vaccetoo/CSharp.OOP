using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private IRepository<IDiver> divers;
        private IRepository<IFish> fishes;


        public Controller()
        {
            divers = new DiverRepository();
            fishes = new FishRepository();
        }


        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            if (!divers.Models.Any(d => d.Name == diverName))
            {
                return $"{typeof(DiverRepository).Name} has no {diverName} registered for the competition.";
            }

            if (!fishes.Models.Any(f => f.Name == fishName))
            {
                return $"{fishName} is not allowed to be caught in this competition.";
            }

            IDiver diver = divers.GetModel(diverName);

            if (diver.HasHealthIssues)
            {
                return $"{diverName} will not be allowed to dive, due to health issues.";
            }

            IFish fish = fishes.GetModel(fishName);

            if (diver.OxygenLevel < fish.TimeToCatch)
            {
                diver.Miss(fish.TimeToCatch);

                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return $"{diverName} missed a good {fishName}.";
            }
            else if (diver.OxygenLevel == fish.TimeToCatch && !isLucky)
            {
                diver.Miss(fish.TimeToCatch);

                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return $"{diverName} missed a good {fishName}.";

            }
            else
            {
                diver.Hit(fish);

                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return $"{diverName} hits a {fish.Points}pt. {fishName}.";
            }
        }

        public string CompetitionStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("**Nautical-Catch-Challenge**");

            foreach (IDiver diver in divers.Models
                .Where(d => d.HasHealthIssues == false)
                .OrderByDescending(d => d.CompetitionPoints)
                .ThenByDescending(d => d.Catch.Count)
                .ThenBy(d => d.Name))
            {
                sb.AppendLine(diver.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            Type type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == diverType);

            if (type == null)
            {
                return $"{diverType} is not allowed in our competition."
;
            }

            if (divers.Models.Any(d => d.Name == diverName))
            {
                return $"{diverName} is already a participant -> {typeof(DiverRepository).Name}.";
            }


            IDiver diver = Activator.CreateInstance(type, diverName) as IDiver;

            divers.AddModel(diver);

            return $"{diverName} is successfully registered for the competition -> {typeof(DiverRepository).Name}.";
        }

        public string DiverCatchReport(string diverName)
        {
            IDiver diver = divers.GetModel(diverName);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(diver.ToString());
            sb.AppendLine("Catch Report:");

            foreach (string fish in diver.Catch)
            {
                IFish currFish = fishes.GetModel(fish);
                sb.AppendLine(currFish.ToString());
            }

            return sb.ToString().Trim().TrimEnd();
        }

        public string HealthRecovery()
        {
            int recoverdDiversCount = 0;

            foreach (IDiver diver in divers.Models.Where(d => d.HasHealthIssues))
            {
                recoverdDiversCount++;

                diver.UpdateHealthStatus();
                diver.RenewOxy();
            }

            return $"Divers recovered: {recoverdDiversCount}";
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            Type type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == fishType);

            if (type == null)
            {
                return $"{fishType} is forbidden for chasing in our competition.";
            }

            if (fishes.Models.Any(f => f.Name == fishName))
            {
                return $"{fishName} is already allowed -> {typeof(FishRepository).Name}.";
            }

            IFish fish = Activator.CreateInstance(type, fishName, points) as IFish;

            fishes.AddModel(fish);

            return $"{fishName} is allowed for chasing.";
        }
    }
}
