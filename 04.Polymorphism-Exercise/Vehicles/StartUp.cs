
using Vehicles.Core;
using Vehicles.Factories;
using Vehicles.Factories.Interfaces;

IVehicleFactory vehicleFactory = new VehicleFactory();

Engine engine = new Engine(vehicleFactory);
engine.Run();