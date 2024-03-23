using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
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
        private IRepository<IDiver> diverRepository;
        private IRepository<IFish> fishRepository;

        public Controller()
        {
            diverRepository = new DiverRepository();
            fishRepository = new FishRepository();
        }

        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            IReadOnlyCollection<IDiver> divers = diverRepository.Models;
            IReadOnlyCollection<IFish> allFish = fishRepository.Models;

            if (!divers.Any(d => d.Name == diverName))
            {
                return $"{typeof(DiverRepository).Name} has no {diverName} registered for the competition.";
            }

            if (!allFish.Any(f => f.Name == fishName))
            {
                return $"{fishName} is not allowed to be caught in this competition.";
            }

            IDiver diver = divers.First(d => d.Name == diverName);

            if (diver.HasHealthIssues)
            {
                return $"{diverName} will not be allowed to dive, due to health issues.";
            }

            IFish fish = allFish.First(f => f.Name == fishName);

            if (diver.OxygenLevel < fish.TimeToCatch)
            {
                diver.Miss(fish.TimeToCatch);

                return $"{diverName} missed a good {fishName}.";
            }
            else if (diver.OxygenLevel == fish.TimeToCatch)
            {
                if (isLucky)
                {
                    diver.Hit(fish);

                    return $"{diverName} hits a {fish.Points}pt. {fishName}.";
                }
                else
                {
                    diver.Miss(fish.TimeToCatch);

                    return $"{diverName} missed a good {fishName}.";
                }
            }
            else
            {
                diver.Hit(fish);

                return $"{diverName} hits a {fish.Points}pt. {fishName}.";
            }


        }

        public string CompetitionStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("**Nautical-Catch-Challenge**");

            foreach (IDiver diver in diverRepository.Models
                .OrderByDescending(d => d.CompetitionPoints)
                .ThenByDescending(d => d.Catch.Count)
                .ThenBy(d => d.Name)
                .Where(d => d.HasHealthIssues == false))
            {
                sb.AppendLine(diver.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            if (diverType != "FreeDiver" && diverType != "ScubaDiver")
            {
                return $"{diverType} is not allowed in our competition.";
            }

            IReadOnlyCollection<IDiver> divers = diverRepository.Models;

            if (divers.Any(d => d.Name == diverName))
            {
                return $"{diverName} is already a participant -> {typeof(DiverRepository).Name}.";
            }

            Type type = Assembly
                .GetEntryAssembly()
                .GetTypes()
                .First(t => t.Name == diverType);

            IDiver diver = Activator.CreateInstance(type, diverName) as IDiver;

            diverRepository.AddModel(diver);

            return $"{diverName} is successfully registered for the competition -> {typeof(DiverRepository).Name}.";
        }

        public string DiverCatchReport(string diverName)
        {
            IDiver diver = diverRepository.GetModel(diverName);

            IReadOnlyCollection<string> caughtFish = diver.Catch;
            IReadOnlyCollection<IFish> allFish = fishRepository.Models;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(diver.ToString());
            sb.AppendLine("Catch Report:");

            foreach (var fish in caughtFish)
            {
                foreach (var repoFish in allFish.Where(rF => rF.Name == fish))
                {
                    sb.AppendLine(repoFish.ToString());
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string HealthRecovery()
        {
            IReadOnlyCollection<IDiver> divers = diverRepository.Models;

            int diversCounter = 0;

            foreach (IDiver diver in divers)
            {
                if (diver.HasHealthIssues)
                {
                    diversCounter++;

                    diver.UpdateHealthStatus();
                    diver.RenewOxy();
                }
            }

            return $"Divers recovered: {diversCounter}";
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            if (fishType != "ReefFish" && fishType != "DeepSeaFish" && fishType != "PredatoryFish")
            {
                return $"{fishType} is forbidden for chasing in our competition.";
            }

            IReadOnlyCollection<IFish> allFish = fishRepository.Models;

            if (allFish.Any(f => f.Name == fishName))
            {
                return $"{fishName} is already allowed -> {typeof(FishRepository).Name}."
;
            }

            Type type = Assembly
                .GetEntryAssembly()
                .GetTypes()
                .First(t => t.Name == fishType);

            IFish fish = Activator.CreateInstance(type, fishName, points) as IFish;

            fishRepository.AddModel(fish);

            return $"{fishName} is allowed for chasing.";
        }
    }
}
