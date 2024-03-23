﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class PredatoryFish : Fish
    {
        private const int InitialTimeToCatch = 60; // sec

        public PredatoryFish(string name, double points) 
            : base(name, points, InitialTimeToCatch)
        {
        }
    }
}
