using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private IRepository<IUser> users;
        private IRepository<IVehicle> vehicles;
        private IRepository<IRoute> routes;


        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }


        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int routId = routes.GetAll().Count() + 1;

            IRoute excistinRoute = routes
                .GetAll()
                .FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint);

            if (excistinRoute != null)
            {
                if (excistinRoute.Length == length)
                {
                    return $"{startPoint}/{endPoint} - {length} km is already added in our platform.";
                }
                else if (excistinRoute.Length < length)
                {
                    return $"{startPoint}/{endPoint} shorter route is already added in our platform.";
                }
                else if (excistinRoute.Length > length)
                {
                    excistinRoute.LockRoute();
                }
            }

            IRoute newRoute = new Route(startPoint, endPoint, length, routId);
            routes.AddModel(newRoute);

            return $"{startPoint}/{endPoint} - {length} km is unlocked in our platform.";
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route = routes.FindById(routeId);

            //  The Vehicle will always have enough battery to finish the trip.

            if (user.IsBlocked)
            {
                return $"User {drivingLicenseNumber} is blocked in the platform! Trip is not allowed.";
            }

            if (vehicle.IsDamaged)
            {
                return $"Vehicle {licensePlateNumber} is damaged! Trip is not allowed.";
            }

            if (route.IsLocked)
            {
                return $"Route {routeId} is locked! Trip is not allowed.";
            }

            vehicle.Drive(route.Length);

            if (isAccidentHappened)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
            }

            return vehicle.ToString();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.GetAll().Any(u => u.DrivingLicenseNumber == drivingLicenseNumber))
            {
                return $"{drivingLicenseNumber} is already registered in our platform.";
            }

            IUser newUser = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(newUser);

            return $"{firstName} {lastName} is registered successfully with DLN-{drivingLicenseNumber}";
        }

        public string RepairVehicles(int count)
        {
            var damagedVehicles = vehicles.GetAll()
                .Where(v => v.IsDamaged)
                .OrderBy(v => v.Brand)
                .ThenBy(v => v.Model);

            int repairCounter = 0;

            if (damagedVehicles.Count() > count )
            {
                repairCounter = count;
            }
            else
            {
                repairCounter = damagedVehicles.Count();
            }

            var vehiclesTorepair = damagedVehicles.Take(repairCounter).ToList();

            foreach (var vehicle in vehiclesTorepair)
            {
                vehicle.ChangeStatus();
                vehicle.Recharge();
            }

            return $"{repairCounter} vehicles are successfully repaired!";
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != nameof(CargoVan) && vehicleType != nameof(PassengerCar))
            {
                return $"{vehicleType} is not accessible in our platform.";
            }

            if (vehicles.FindById(licensePlateNumber) != null)
            {
                return $"{licensePlateNumber} belongs to another vehicle.";
            }

            IVehicle newVehicle = vehicleType switch
            {
                "CargoVan" => new CargoVan(brand, model, licensePlateNumber),
                "PassengerCar" => new PassengerCar(brand, model, licensePlateNumber),
            };

            vehicles.AddModel(newVehicle);

            return $"{brand} {model} is uploaded successfully with LPN-{licensePlateNumber}";
        }

        public string UsersReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*** E-Drive-Rent ***");

            foreach (var user in users.GetAll()
                .OrderByDescending(u => u.Rating)
                .ThenBy(u => u.LastName)
                .ThenBy(u => u.FirstName))
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
