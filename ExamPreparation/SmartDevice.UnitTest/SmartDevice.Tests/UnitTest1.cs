namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tests
    {
        [Test]
        public void ConstructorSavesDataCorrect()
        {
            Device device = new Device(100);

            int expMemoryCapacity = 100;
            int actMemoryCapacity = device.MemoryCapacity;

            int expAvailableMemory = 100;
            int actAvailableMemory = device.AvailableMemory;

            int expPhotos = 0;
            int actPhotos = device.Photos;

            List<string> expApps = new List<string>();
            List<string> actApps = device.Applications;

            Assert.AreEqual(expMemoryCapacity, actMemoryCapacity);
            Assert.AreEqual(expAvailableMemory, actAvailableMemory);
            Assert.AreEqual(expPhotos, actPhotos);
            Assert.AreEqual(expApps, actApps);
        }

        [Test]
        public void TakePhotoDecreeseAvailableMemory()
        {
            Device device = new(100);
            int photoSize = 50;

            device.TakePhoto(photoSize);

            int expAvailableCapacity = 50;
            int actualAvailableCapacity = device.AvailableMemory;

            Assert.AreEqual(expAvailableCapacity, actualAvailableCapacity);
        }


        [Test]
        public void TakePhotoIncreesePhotoCount()
        {
            Device device = new(100);

            device.TakePhoto(50);

            int expPhotoCnt = 1;
            int actPhotoCnt = device.Photos;

            Assert.AreEqual(expPhotoCnt, actPhotoCnt);
        }


        [Test]
        public void TakePhotoReturnTrueIfPhotoSizeInRange()
        {
            Device device = new(100);

            bool expResult = true;
            bool actresult = device.TakePhoto(50);

            Assert.AreEqual(expResult, actresult);
        }


        [Test]
        public void TakePhotoReturnFalseIfPhosizeOutOfrange()
        {
            Device device = new(100);

            bool expResult = false;
            bool actResult = device.TakePhoto(101);

            Assert.AreEqual(expResult, actResult);
        }


        [Test]
        public void InstallAppDecreeseAvailableMemory()
        {
            Device device = new(100);

            device.InstallApp("app", 50);

            int expAvailableMemory = 50;
            int actualAvailableMemory = device.AvailableMemory;

            Assert.AreEqual(expAvailableMemory, actualAvailableMemory);
        }


        [Test]
        public void InstallAppAddsToApplications()
        {
            Device device = new(100);

            device.InstallApp("app", 50);

            List<string> expApps = new List<string>()
            {
                "app"
            };

            List<string> actApps = device.Applications;

            Assert.AreEqual(expApps.Count, actApps.Count);
        }


        [Test]
        public void InstallAppreturnCorrectMessage()
        {
            Device device = new(100);
            
            string expResult = $"app is installed successfully. Run application?";
            string actresult = device.InstallApp("app", 50);

            Assert.AreEqual(expResult, actresult);
        }


        [Test]
        public void InstallAppThrowsExceptionWhenMemoryIsNotEnough()
        {
            Device device = new(100);

            Assert.Throws<InvalidOperationException>(() => device.InstallApp("app", 101), "Not enough available memory to install the app.");
        }


        [Test]
        public void FormatDeviceSetsPhotoToZero()
        {
            Device device = new(100);
            device.TakePhoto(20);

            device.FormatDevice();

            int expresult = 0;
            int actResult = device.Photos;

            Assert.AreEqual(expresult, actResult);
        }


        [Test]
        public void FormatDeviceSetsApplicationsToZero()
        {
            Device device = new(100);
            device.InstallApp("app", 20);

            device.FormatDevice();

            List<string> expresult = new List<string>();
            List<string> actresult = device.Applications;

            Assert.AreEqual(expresult, actresult);
        }


        [Test]
        public void FormatDeviceSetsAvailableMemorytomemorycapacity()
        {
            Device device = new(100);
            device.TakePhoto(20);
            device.InstallApp("app", 10);

            device.FormatDevice();

            int expresult = 100;
            int actualresult = device.AvailableMemory;

            Assert.AreEqual(expresult, actualresult);
        }


        [Test]
        public void GetDeviceStatusreturnCorrectString()
        {
            Device device = new(100);

            StringBuilder expResult = new StringBuilder();

            expResult.AppendLine($"Memory Capacity: {device.MemoryCapacity} MB, Available Memory: {device.AvailableMemory} MB");
            expResult.AppendLine($"Photos Count: {device.Photos}");
            expResult.AppendLine($"Applications Installed: {string.Join(", ", device.Applications)}");

            string actResult = device.GetDeviceStatus();

            Assert.AreEqual(expResult.ToString().TrimEnd(), actResult);
        }
    }
}
