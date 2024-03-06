using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Core.Interfaces;
using Vehicles.Factories.Interfaces;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private readonly IVehicleFactory vehicleFactory;

        private readonly ICollection<IVehicle> vehicles;


        public Engine(IVehicleFactory vehicleFactory)
        {
            this.vehicleFactory = vehicleFactory;

            vehicles = new List<IVehicle>();
        }


        public void Run()
        {
            vehicles.Add(CreateVehicle());
            vehicles.Add(CreateVehicle());
            vehicles.Add(CreateVehicle());

            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                string[] commandInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string commandName = commandInfo[0];
                string vehicleType = commandInfo[1];
                double value = double.Parse(commandInfo[2]);

                IVehicle currentVehicle = vehicles.First(v => v.GetType().Name == vehicleType);

                try
                {
                    if (commandName == "Drive")
                    {
                        Console.WriteLine(currentVehicle.Drive(value));
                    }
                    else if (commandName == "DriveEmpty")
                    {
                        Console.WriteLine(currentVehicle.Drive(value, false));
                    }
                    else if (commandName == "Refuel")
                    {
                        currentVehicle.Refuel(value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        private IVehicle CreateVehicle()
        {
            string[] currentVehicleInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string vehicleType = currentVehicleInfo[0]; 
            double fuelQuantity = double.Parse(currentVehicleInfo[1]);
            double fuelConsumption = double.Parse(currentVehicleInfo[2]);
            double fuelTank = double.Parse(currentVehicleInfo[3]);

            return vehicleFactory.Create(vehicleType, fuelQuantity, fuelConsumption, fuelTank);
        }
    }
}
