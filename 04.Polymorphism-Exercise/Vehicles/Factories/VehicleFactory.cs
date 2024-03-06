using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Factories.Interfaces;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles.Factories
{
    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle Create(string vehicleType, double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            if (vehicleType == "Car")
            {
                return new Car(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else if (vehicleType == "Truck")
            {
                return new Truck(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else if(vehicleType == "Bus")
            {
                return new Bus(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else
            {
                throw new ArgumentException("Invalid vehicle");
            }
        }
    }
}
