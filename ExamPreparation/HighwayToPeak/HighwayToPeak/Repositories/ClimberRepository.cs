﻿using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Repositories
{
    public class ClimberRepository : IRepository<IClimber>
    {
        private readonly List<IClimber> climbers;

        public IReadOnlyCollection<IClimber> All 
            => climbers;

        public void Add(IClimber model)
        {
            climbers.Add(model);
        }

        public IClimber Get(string name)
            => climbers.FirstOrDefault(c => c.Name == name);
    }
}
