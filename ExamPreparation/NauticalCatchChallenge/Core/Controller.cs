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
            throw new NotImplementedException();
        }

        public string CompetitionStatistics()
        {
            throw new NotImplementedException();
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

            IDiver diver = null;

            if (diverType == "FreeDiver")
            {
                diver = new FreeDiver(diverName);
                
            }
            else if (diverType == "ScubaDiver")
            {
                diver = new ScubaDiver(diverName);
            }

            diverRepository.AddModel(diver);

            return $"{diverName} is successfully registered for the competition -> {typeof(DiverRepository).Name}.";
        }

        public string DiverCatchReport(string diverName)
        {
            throw new NotImplementedException();
        }

        public string HealthRecovery()
        {
            throw new NotImplementedException();
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
                .First(a => a.Name == fishType);

            IFish fish = Activator.CreateInstance(type, fishName, points) as IFish;

            fishRepository.AddModel(fish);  

            return $"{fishName} is allowed for chasing.";
        }
    }
}
