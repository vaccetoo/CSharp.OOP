using HighwayToPeak.Core.Contracts;
using HighwayToPeak.IO;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Core
{
    public class Controller : IController
    {
        private IRepository<IPeak> peakRepository;
        private IRepository<IClimber> climberRepository;
        private IBaseCamp baseCamp;

        public Controller()
        {
            peakRepository = new PeakRepository();
            climberRepository = new ClimberRepository();
            baseCamp = new BaseCamp();
        }

        public string AddPeak(string name, int elevation, string difficultyLevel)
        {
            IPeak peak = new Peak(name, elevation, difficultyLevel);

            IReadOnlyCollection<IPeak> peaks = peakRepository.All;

            if (peaks.Any(p => p.Name == name))
            {
                return $"{name} is already added as a valid mountain destination.";
            }

            if (peak.DifficultyLevel != "Extreme" &&
                peak.DifficultyLevel != "Hard" &&
                peak.DifficultyLevel != "Moderate")
            {
                return $"{difficultyLevel} peaks are not allowed for international climbers.";
            }

            peakRepository.Add(peak);

            //TODO: Is retyrnType correct
            return $"{name} is allowed for international climbing. See details in {typeof(PeakRepository)}."
;
        }

        public string AttackPeak(string climberName, string peakName)
        {
            throw new NotImplementedException();
        }

        public string BaseCampReport()
        {
            throw new NotImplementedException();
        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            throw new NotImplementedException();
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            throw new NotImplementedException();
        }

        public string OverallStatistics()
        {
            throw new NotImplementedException();
        }
    }
}
