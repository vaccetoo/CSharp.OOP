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
        private IRepository<IUser> userRepository;
        private IRepository<IVehicle> vehicleRepository;
        private IRepository<IRoute> routeRepository;


        public Controller()
        {
            userRepository = new UserRepository();
            vehicleRepository = new VehicleRepository();
            routeRepository = new RouteRepository();
        }


        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int routeId = routeRepository.GetAll().Count + 1;

            if (routeRepository.GetAll().Any(r => r.StartPoint == startPoint 
                                               && r.EndPoint == endPoint 
                                               && r.Length == length))
            {
                return $"{startPoint}/{endPoint} - {length} km is already added in our platform.";
            }

            if (routeRepository.GetAll().Any(r => r.StartPoint == startPoint
                                               && r.EndPoint == endPoint
                                               && r.Length < length))
            {
                return $"{startPoint}/{endPoint} shorter route is already added in our platform.";
            }

            IRoute newRoute = new Route(startPoint, endPoint, length, routeId);
            routeRepository.AddModel(newRoute);

            if (routeRepository.GetAll().Any(r => r.StartPoint == startPoint
                                               && r.EndPoint == endPoint
                                               && r.Length > length))
            {
                routeRepository.GetAll().First(r => r.StartPoint == startPoint
                                               && r.EndPoint == endPoint
                                               && r.Length > length).LockRoute();
            }

            return $"{startPoint}/{endPoint} - {length} km is unlocked in our platform.";
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            if (userRepository.FindById(drivingLicenseNumber).IsBlocked)
            {
                return $"User {drivingLicenseNumber} is blocked in the platform! Trip is not allowed.";
            }

            if (vehicleRepository.FindById(licensePlateNumber).IsDamaged)
            {
                return $"Vehicle {licensePlateNumber} is damaged! Trip is not allowed.";
            }

            if (routeRepository.FindById(routeId).IsLocked)
            {
                return $"Route {routeId} is locked! Trip is not allowed.";
            }

            IUser currentUser = userRepository.FindById(drivingLicenseNumber);
            IVehicle currentVehicle = vehicleRepository.FindById(licensePlateNumber);
            IRoute currentRoute = routeRepository.FindById(routeId);

            currentVehicle.Drive(currentRoute.Length);

            if (isAccidentHappened)
            {
                currentVehicle.ChangeStatus();
                currentUser.DecreaseRating();
            }
            else
            {
                currentUser.IncreaseRating();
            }

            return currentVehicle.ToString();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (userRepository.FindById(drivingLicenseNumber) != null)
            {
                return $"{drivingLicenseNumber} is already registered in our platform.";
            }

            IUser newUser = new User(firstName, lastName, drivingLicenseNumber);
            userRepository.AddModel(newUser);

            return $"{firstName} {lastName} is registered successfully with DLN-{drivingLicenseNumber}";
        }

        public string RepairVehicles(int count)
        {
            IReadOnlyCollection<IVehicle> damegedVehicles = vehicleRepository.GetAll().Where(v => v.IsDamaged).OrderBy(v => v.Brand).ThenBy(v => v.Model).ToList();

            int repairedVehicles = 0;

            if (count < damegedVehicles.Count)
            {
                for (int i = 0; i < count; i++)
                {
                    repairedVehicles++;

                    foreach (var vehicle in damegedVehicles)
                    {
                        vehicle.Recharge();
                        vehicle.ChangeStatus();
                    }
                }
            }
            else
            {
                foreach (var vehicle in damegedVehicles)
                {
                    repairedVehicles++;

                    vehicle.Recharge();
                    vehicle.ChangeStatus();
                }
            }

            return $"{repairedVehicles} vehicles are successfully repaired!";
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            Type type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == vehicleType);

            if (type == null)
            {
                return $"{vehicleType} is not accessible in our platform.";
            }

            if (vehicleRepository.FindById(licensePlateNumber) != null)
            {
                return $"{licensePlateNumber} belongs to another vehicle.";
            }

            IVehicle newVehicle = vehicleType switch
            {
                "PassengerCar" => new PassengerCar(brand, model, licensePlateNumber),
                "CargoVan" => new CargoVan(brand, model, licensePlateNumber),
            };

            vehicleRepository.AddModel(newVehicle);

            return $"{brand} {model} is uploaded successfully with LPN-{licensePlateNumber}";
        }

        public string UsersReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*** E-Drive-Rent ***");

            foreach (var user in userRepository.GetAll()
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
