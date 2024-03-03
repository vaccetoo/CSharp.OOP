using MilitaryElite.Enums;
using MilitaryElite.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MilitaryElite.Models
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        public Commando(string id, string firstName, string lastName, decimal salary, Corps corps, IReadOnlyCollection<Mission> missions)
            : base(id, firstName, lastName, salary, corps)
        {
            Missions = missions;
        }

        public IReadOnlyCollection<Mission> Missions { get; private set;}


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($"Corps: {Corps}");
            sb.AppendLine("Missions:");

            foreach (var mission in Missions)
            {
                sb.AppendLine($" {mission.ToString()}"); 
            }

            return sb.ToString().TrimEnd();
        }
    }
}
