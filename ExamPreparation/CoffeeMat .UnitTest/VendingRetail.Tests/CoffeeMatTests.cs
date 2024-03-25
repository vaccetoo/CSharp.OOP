using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class CoffeeMatTests
    {
        [Test]
        public void ConstructorWorksCorrect()
        {
            CoffeeMat coffeeMat = new(100, 10);

            Assert.AreEqual(100, coffeeMat.WaterCapacity);
            Assert.AreEqual(10, coffeeMat.ButtonsCount);
            Assert.AreEqual(0, coffeeMat.Income);
            Assert.IsNotNull(coffeeMat);
        }


        [Test]
        public void FillWaterTankReturnWaterTankWorksCorrect()
        {
            CoffeeMat coffeeMat = new(100, 10);

            string result = coffeeMat.FillWaterTank();

            Assert.AreEqual("Water tank is filled with 100ml", result);
        }

        [Test]
        public void FillWaterTankReturnFullMessage()
        {
            CoffeeMat coffeeMat = new(100, 10);

            coffeeMat.FillWaterTank();
            string result = coffeeMat.FillWaterTank();

            Assert.AreEqual("Water tank is already full!", result);
        }

        [Test]
        public void AddDrinkReturnsTrue()
        {
            CoffeeMat coffeeMat = new(10, 10);

            bool result = coffeeMat.AddDrink("coffee", 5);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddDrinkReturnsFalseIfButtonsCount()
        {
            CoffeeMat coffeeMat = new(10, 1);

            coffeeMat.AddDrink("tea", 10);
            bool result = coffeeMat.AddDrink("milk", 12);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddDrinkReturnsFalseIfDrinkExsist()
        {
            CoffeeMat coffeeMat = new(10, 2);

            coffeeMat.AddDrink("tea", 10);
            bool result = coffeeMat.AddDrink("tea", 20);

            Assert.IsFalse(result);
        }


        [Test]
        public void BuyDrinkWaterTankOutofWater()
        {
            CoffeeMat coffeeMat = new(10, 10);

            coffeeMat.AddDrink("tea", 1);

            string result = coffeeMat.BuyDrink("tea");

            Assert.AreEqual("CoffeeMat is out of water!", result);
        }

        [Test]
        public void BuyDrinkIcreeseIncomeIfDrinkExsist()
        {
            CoffeeMat coffeeMat = new(90, 10);
            coffeeMat.FillWaterTank();

            coffeeMat.AddDrink("drink", 1.2);

            string result = coffeeMat.BuyDrink("drink");

            Assert.AreEqual("Your bill is 1.20$", result);
            Assert.AreEqual(1.20, coffeeMat.Income);
        }


        [Test]
        public void BuyDrinkDrinkIsNotAvailable()
        {
            CoffeeMat coffeeMat = new(90, 10);
            coffeeMat.FillWaterTank();

            coffeeMat.AddDrink("drink", 1);

            string result = coffeeMat.BuyDrink("water");

            Assert.AreEqual("water is not available!", result);
        }


        [Test]
        public void CollectIncome()
        {
            CoffeeMat coffeeMat = new(500, 10);

            coffeeMat.FillWaterTank();

            coffeeMat.AddDrink("drink", 1);
            coffeeMat.AddDrink("water", 2);

            coffeeMat.BuyDrink("drink");
            coffeeMat.BuyDrink("water");

            double actualIncome = coffeeMat.Income;
            double income = coffeeMat.CollectIncome();
            double incomeAfter = coffeeMat.Income;

            Assert.AreEqual(3, actualIncome);
            Assert.AreEqual(3, income);
            Assert.AreEqual(0, incomeAfter);
            
        }


        [Test]
        public void CheckWaterConsuming()
        {
            CoffeeMat coffeeMat = new CoffeeMat(2000, 6);

            coffeeMat.FillWaterTank();

            coffeeMat.AddDrink("Coffee", 0.80);
            coffeeMat.AddDrink("Macciato", 1.80);
            coffeeMat.AddDrink("Capuccino", 1.50);
            coffeeMat.AddDrink("Latte", 1.00);
            coffeeMat.AddDrink("Hot Chocolate", 1.60);
            coffeeMat.AddDrink("Milk", 0.90);
            coffeeMat.AddDrink("Tea", 0.60);
            coffeeMat.AddDrink("Hot Water", 0.30);

            coffeeMat.BuyDrink("Coffee");
            coffeeMat.BuyDrink("Macciato");
            coffeeMat.BuyDrink("Capuccino");
            coffeeMat.BuyDrink("Latte");
            coffeeMat.BuyDrink("Milk");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            coffeeMat.BuyDrink("Hot Chocolate");
            string actualResult = coffeeMat.BuyDrink("Hot Chocolate");

            string expectedResult = "CoffeeMat is out of water!";

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}