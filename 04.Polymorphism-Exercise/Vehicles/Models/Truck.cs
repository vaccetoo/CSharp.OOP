using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double IncreasedConsumption = 1.6;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity, IncreasedConsumption)
        {
        }

        public override void Refuel(double amount)
            => base.Refuel(amount * 0.95);

        public override string ToString()
            => $"Truck: {FuelQuantity:f2}";
    }
}
