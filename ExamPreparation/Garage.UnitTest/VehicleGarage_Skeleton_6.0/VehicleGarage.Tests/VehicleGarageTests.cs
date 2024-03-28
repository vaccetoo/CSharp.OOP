using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;

namespace VehicleGarage.Tests
{
    public class VehicleGarageTests
    {
        [Test]
        public void TestVehicleConstructor()
        {
            Vehicle vehicle = new("Brand", "Model", "Number");

            Assert.IsNotNull(vehicle);
            Assert.AreEqual("Brand", vehicle.Brand);
            Assert.AreEqual("Model", vehicle.Model);
            Assert.AreEqual("Number", vehicle.LicensePlateNumber);
            Assert.AreEqual(100, vehicle.BatteryLevel);
            Assert.IsFalse(vehicle.IsDamaged);
        }

        [Test]
        public void TestGarageConstructor()
        {
            Garage garage = new(100);

            Assert.IsNotNull(garage);
            Assert.AreEqual(100, garage.Capacity);

            List<Vehicle> expectedVehicles = new List<Vehicle>();

            Assert.AreEqual(expectedVehicles, garage.Vehicles);
        }

        [Test]
        public void AddVehicleReturnstrue()
        {
            Garage garage = new(100);
            Vehicle vehicle = new("Brand", "Model", "Number");

            Assert.IsTrue(garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void AddVehicleReturnsFalseIfCapacityEqualsToVehiclesCount()
        {
            Garage garage = new(3);
            Vehicle vehicle1 = new("Brand1", "Model1", "Number1");
            Vehicle vehicle2 = new("Brand2", "Model2", "Number2");
            Vehicle vehicle3 = new("Brand3", "Model3", "Number3");

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);

            Vehicle vehicle4 = new("Brand4", "Model4", "Number4");

            Assert.IsFalse(garage.AddVehicle(vehicle4));
        }

        [Test]
        public void AddvehiclereturnsFalseIfExcistedVehicle()
        {
            Garage garage = new(3);
            Vehicle vehicle1 = new("Brand1", "Model1", "Number1");
            Vehicle vehicle2 = new("Brand2", "Model2", "Number2");

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            Assert.IsFalse(garage.AddVehicle(new Vehicle("Brand123", "Model1234", "Number1")));
        }

        [TestCase(50)]
        [TestCase(60)]
        public void ChargedvehiclesReturnCorrect(int batteryDrainage)
        {
            Garage garage = new(3);
            Vehicle vehicle1 = new("Brand1", "Model1", "Number1");
            Vehicle vehicle2 = new("Brand2", "Model2", "Number2");

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("Number1", batteryDrainage, true);

            Assert.AreEqual(1, garage.ChargeVehicles(batteryDrainage));
        }

        [TestCase(50)]
        [TestCase(30)]
        public void DriveVehicleDecreesBatteryLevelByCorrectAmount(int batteryDrainage)
        {
            Garage garage = new(3);
            Vehicle vehicle1 = new("Brand1", "Model1", "Number1");
            Vehicle vehicle2 = new("Brand2", "Model2", "Number2");

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("Number1", batteryDrainage, true);

            Assert.AreEqual(100 - batteryDrainage, vehicle1.BatteryLevel);
            Assert.IsTrue(vehicle1.IsDamaged);
        }

        [Test]
        public void DriveVehicleReturnsIfIsDamaged()
        {
            Garage garage = new(3);
            Vehicle vehicle1 = new("Brand1", "Model1", "Number1");
            Vehicle vehicle2 = new("Brand2", "Model2", "Number2");

            garage.AddVehicle(vehicle1);

            garage.DriveVehicle("Number1", 50, true);

            garage.DriveVehicle("Number1", 50, false);

            Assert.AreEqual(50, vehicle1.BatteryLevel);
        }

        [Test]
        public void DriveVehicleReturnsIfBatteryDrainageGreaterThan100()
        {
            Garage garage = new(3);
            Vehicle vehicle1 = new("Brand1", "Model1", "Number1");
            Vehicle vehicle2 = new("Brand2", "Model2", "Number2");

            garage.AddVehicle(vehicle1);

            garage.DriveVehicle("Number1", 101, false);

            Assert.AreEqual(100, vehicle1.BatteryLevel);
        }

        [Test]
        public void DriveVehicleReturnsIfBatterylevelLessThanBatteryDrainage()
        {
            Garage garage = new(3);
            Vehicle vehicle1 = new("Brand1", "Model1", "Number1");
            Vehicle vehicle2 = new("Brand2", "Model2", "Number2");

            garage.AddVehicle(vehicle1);

            garage.DriveVehicle("Number1", 50, false);
            garage.DriveVehicle("Number1", 51, false);

            Assert.AreEqual(50, vehicle1.BatteryLevel);
        }

        [Test]
        public void RepairvehiclesreturnsCorrectMessage()
        {
            Garage garage = new(3);
            Vehicle vehicle1 = new("Brand1", "Model1", "Number1");
            Vehicle vehicle2 = new("Brand2", "Model2", "Number2");

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            vehicle1.IsDamaged = true;
            vehicle2.IsDamaged = true;

            Assert.AreEqual("Vehicles repaired: 2", garage.RepairVehicles());
        }
    }
}