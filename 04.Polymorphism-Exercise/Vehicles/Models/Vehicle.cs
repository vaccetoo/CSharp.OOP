using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        private double increasedConsumption;
        private double fuelQuantity;


        public Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity, double increasedConsumption)
        {
            FuelConsumption = fuelConsumption;
            FuelQuantity = fuelQuantity;
            TankCapacity = tankCapacity;
            this.increasedConsumption = increasedConsumption;
        }


        public double FuelQuantity
        {
            get => fuelQuantity;

            protected set
            {
                if (FuelQuantity <= TankCapacity)
                {
                    fuelQuantity = value;
                }
                else
                {
                    fuelQuantity = 0;
                }
            }
        }

        public double FuelConsumption { get; private set; }

        public double TankCapacity { get; private set; }

        public string Drive(double distance)
        {
            if (FuelQuantity >= (FuelConsumption + increasedConsumption) * distance)
            {
                FuelQuantity -= (FuelConsumption + increasedConsumption) * distance;

                return $"{GetType().Name} travelled {distance} km";
            }

            throw new ArgumentException($"{GetType().Name} needs refueling");
        }

        public virtual void Refuel(double amount)
        {
            if (amount > 0)
            {
                if (FuelQuantity + amount <= TankCapacity)
                {
                    FuelQuantity += amount;
                }
                else
                {
                    throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
                }
            }
            else
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
        }
    }
}
