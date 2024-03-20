namespace Television.Tests
{
    using System;
    using NUnit.Framework;
    public class Tests
    {
        [TestCase("tv", 1.1, 1, 2)]
        public void TestConstructorSavesCorrectlyData(string brand, double price, int screenWidth, int screenHeigth)
        {
            TelevisionDevice tv = new(brand, price, screenWidth, screenHeigth);

            Assert.AreEqual(brand, tv.Brand);
            Assert.AreEqual(price, tv.Price);
            Assert.AreEqual(screenWidth, tv.ScreenWidth);
            Assert.AreEqual(screenHeigth, tv.ScreenHeigth);
        }

        [TestCase()]
        
    }
}