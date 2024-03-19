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
using System.Xml.Linq;

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

            return $"{name} is allowed for international climbing. See details in {typeof(PeakRepository).Name}."
;
        }

        public string AttackPeak(string climberName, string peakName)
        {
            IReadOnlyCollection<IClimber> climbers = climberRepository.All;
            IReadOnlyCollection<IPeak> peaks = peakRepository.All;
            IReadOnlyCollection<string> bCamp = baseCamp.Residents;

            if (!climbers.Any(c => c.Name == climberName))
            {
                return $"Climber - {climberName}, has not arrived at the BaseCamp yet.";
            }

            if (!peaks.Any(p => p.Name == peakName))
            {
                return $"{peakName} is not allowed for international climbing.";
            }

            if (!bCamp.Contains(climberName))
            {
                return $"{climberName} not found for gearing and instructions. The attack of {peakName} will be postponed.";
            }

            IPeak currentPeak = peaks.First(p => p.Name == peakName);
            IClimber currentClimber = climbers.First(c => c.Name == climberName);

            if (currentPeak.DifficultyLevel == "Extreme" && currentClimber.GetType().Name == "NaturalClimber")
            {
                return $"{climberName} does not cover the requirements for climbing {peakName}.";
            }

            baseCamp.LeaveCamp(climberName);
            currentClimber.Climb(currentPeak);

            if (currentClimber.Stamina <= 0)
            {
                return $"{climberName} did not return to BaseCamp.";
            }
            else
            {
                baseCamp.ArriveAtCamp(climberName);

                return $"{climberName} successfully conquered {peakName} and returned to BaseCamp.";
            }

        }

        public string BaseCampReport()
        {
            StringBuilder sb = new StringBuilder();

            IReadOnlyCollection<string> bCamp = baseCamp.Residents;
            IReadOnlyCollection<IClimber> climbers = climberRepository.All;

            if (bCamp.Count > 0)
            {
                sb.AppendLine($"BaseCamp residents:");

                foreach (string climberInBaseCamp in bCamp)
                {
                    foreach (IClimber climber in climbers.Where(c => c.Name == climberInBaseCamp))
                    {
                        sb.AppendLine($"Name: {climber.Name}, Stamina: {climber.Stamina}, Count of Conquered Peaks: {climber.ConqueredPeaks.Count}");
                    }
                }

                return sb.ToString().TrimEnd();
            }

            return $"BaseCamp is currently empty.";
        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            IReadOnlyCollection<string> bCamp = baseCamp.Residents;
            IReadOnlyCollection<IClimber> climbers = climberRepository.All;

            IClimber currentClimber = climbers.First(c => c.Name == climberName);

            if (!bCamp.Contains(climberName))
            {
                return $"{climberName} not found at the BaseCamp.";
            }

            if (currentClimber.Stamina == 10)
            {
                return $"{climberName} has no need of recovery.";
            }

            currentClimber.Rest(daysToRecover);
            return $"{climberName} has been recovering for {daysToRecover} days and is ready to attack the mountain.";
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            IClimber climber;

            if (isOxygenUsed)
            {
                climber = new OxygenClimber(name);
            }
            else
            {
                climber = new NaturalClimber(name);
            }

            IReadOnlyCollection<IClimber> climbers = climberRepository.All;

            if (climbers.Any(c => c.Name == name))
            {
                return $"{name} is a participant in {typeof(ClimberRepository).Name} and cannot be duplicated.";
            }

            climberRepository.Add(climber);
            baseCamp.ArriveAtCamp(climber.Name);

            return $"{name} has arrived at the BaseCamp and will wait for the best conditions.";
        }

        public string OverallStatistics()
        {
            IReadOnlyCollection<IClimber> climbers = climberRepository.All;
            IReadOnlyCollection<IPeak> currentPeaks = peakRepository.All;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***Highway-To-Peak***");

            foreach (IClimber climber in climbers
                .OrderByDescending(c => c.ConqueredPeaks.Count)
                .ThenBy(c => c.Name))
            {
                sb.AppendLine(climber.ToString());

                foreach (IPeak peak in currentPeaks
                    .OrderByDescending(p => p.Elevation))
                {
                    foreach (string item in climber.ConqueredPeaks.Where(item => item == peak.Name))
                    {
                        sb.AppendLine(peak.ToString());
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
