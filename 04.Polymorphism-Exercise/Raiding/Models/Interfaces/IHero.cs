using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models.Interfaces
{
    public interface IHero
    {
        public string Name { get; }

        public int Power { get; }

        string CastAbility();
    }
}
