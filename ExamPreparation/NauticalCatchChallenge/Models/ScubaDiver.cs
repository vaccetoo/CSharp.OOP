using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class ScubaDiver : Diver
    {
        private const int InitialOxygenLevel = 540;


        public ScubaDiver(string name) 
            : base(name, InitialOxygenLevel)
        {
        }


        public override void Miss(int TimeToCatch)
        {
            this.OxygenLevel -= (int)Math.Round(0.3 * TimeToCatch, MidpointRounding.AwayFromZero);
        }

        public override void RenewOxy()
        {
            this.OxygenLevel = InitialOxygenLevel;
        }
    }
}
