﻿using EDriveRent.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class CargoVan : Vehicle
    {
        private const double InitialMaxMileage = 180;


        public CargoVan(string brand, string model, string licensePlateNumber) 
            : base(brand, model, InitialMaxMileage, licensePlateNumber)
        {
        }

    }
}