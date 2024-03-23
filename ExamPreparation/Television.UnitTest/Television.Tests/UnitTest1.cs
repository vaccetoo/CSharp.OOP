namespace Television.Tests
{
    using System;
    using System.Diagnostics;
    using System.Net.Http.Headers;
    using NUnit.Framework;
    public class Tests
    {
        private readonly TelevisionDevice tv = new ("TV", 1.1, 1, 1);
       
        [Test]
        public void TestingConstructor()
        {
            string expectedBrand = "TV";
            double expectedPrice = 1.1;
            int expectedWidth = 1;
            int expectedHeigth = 1;

            Assert.AreEqual(expectedBrand, tv.Brand);
            Assert.AreEqual(expectedPrice, tv.Price);
            Assert.AreEqual(expectedWidth, tv.ScreenWidth);
            Assert.AreEqual(expectedHeigth, tv.ScreenHeigth);
        }

        [Test]
        public void TestCurrentChannelGetteris0()
        {
            int expectedResult = 0;
            int actualResult = tv.CurrentChannel;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestingVolumeGetterIs13()
        {
            int expectedResult = 13;
            int actualResult = tv.Volume;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CheckIsTVMutedShouldReturnFalse()
        {
            bool expectedResult = false;
            bool actualResult = tv.IsMuted;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void SwitchOnShouldReturnCorrectString()
        {
            string expectedResult = "Cahnnel 0 - Volume 13 - Sound On";
            string actualResult = tv.SwitchOn();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(22)]
        [TestCase(1)]
        [TestCase(111)]
        public void ChangeChannelReturnCorrectResult(int newChanel)
        {
            TelevisionDevice television = new("TV2", 1.1, 1, 1);
            
            int expectedResult = newChanel;
            int actualResult = television.ChangeChannel(newChanel);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(-1)]
        [TestCase(-333)]
        public void ChangeChanelThrowsExceptionIfChanelLessThan0(int newChanel)
        {
            TelevisionDevice television = new("TV2", 1.1, 1, 1);

            Assert.Throws<ArgumentException>(() => television.ChangeChannel(newChanel), "Invalid key!");
        }

        [TestCase(88)]
        [TestCase(89)]
        public void VolumeChangeReturnCorrectStringIfUpMoreThan100(int units)
        {
            TelevisionDevice television = new("TV2", 1.1, 1, 1);

            string expectedResult = "Volume: 100";
            string actualResult = television.VolumeChange("UP", units);

            Assert.AreEqual(expectedResult, actualResult);  
        }

        [TestCase(14)]
        [TestCase(-15)]
        public void VolumeChangeReturnCorrectStringIfLessThan0(int units)
        {
            TelevisionDevice television = new("TV2", 1.1, 1, 1);

            string expectedResult = "Volume: 0";
            string actualResult = television.VolumeChange("DOWN", units);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(10)]
        public void VolumeChangeReturnCorrectStringIfWorksRegular(int units)
        {
            TelevisionDevice television = new("TV2", 1.1, 1, 1);

            string expectedResult = "Volume: 23";
            string actualResult = television.VolumeChange("UP", units);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void MuteDeviceReturnCorrectBool()
        {
            TelevisionDevice television = new("TV2", 1.1, 1, 1);

            bool expectedResult = true;
            bool actualResult = television.MuteDevice();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestToStringOverrrideIsCoorect()
        {
            TelevisionDevice television = new("TV2", 1.1, 1, 1);

            Assert.AreEqual($"TV Device: TV2, Screen Resolution: 1x1, Price 1.1$", television.ToString());
        }

    }
}