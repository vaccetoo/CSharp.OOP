﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        private const double IncreasedConsumption = 1.4;

        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity, IncreasedConsumption)
        {
        }

        public string DriveEmptyBus(double distance)
        {
            if (FuelQuantity <= FuelConsumption * distance)
            {
                FuelQuantity -= FuelConsumption * distance;

                return $"{GetType().Name} travelled {distance} km";
            }

            throw new ArgumentException($"{GetType().Name} needs refueling");
        }


        public override string ToString()
            => $"Bus: {FuelQuantity:f2}";
    }
}
