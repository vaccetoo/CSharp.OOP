namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;

    [TestFixture]
    public class DatabaseTests
    {
        private const int DataBaseCapacity = 16;
        private Database database;

        [TestCase(new int[] {1, 2, 3, 4, 5})]
        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7})]
        [TestCase(new int[] {1})]
        public void AddedDataBaseHasCorrectCount(int[] addedData)
        {
            database = new(addedData);

            int expectedResult = addedData.Length;
            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })]
        public void AddedDataBaseHasNotCorrectCount(int[] addedData)
        {
            Assert.Throws<InvalidOperationException>(() => database = new(addedData), "Array's capacity must be exactly 16 integers!");
        }


        [TestCase(new int[] {1, 2})]
        public void CountGetterWorksCorrectly(int[] addedData)
        {
            database = new(addedData);

            int expectedResult = 2;
            int actualResult = database.Count;  

            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestCase(new int[] { 1, 2 })]
        public void AddMethodAddsCorrectly(int[] addedData)
        {
            database = new(addedData);
            database.Add(3);

            int[] expectedResult = new int[] { 1, 2, 3 };
            int[] actualResult = database.Fetch();

            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16})]
        public void AddMethodThrowsExeptionIfMoreThanDataBaseCapacity(int[] addedData)
        {
            database = new(addedData);

            Assert.Throws<InvalidOperationException>(() => database.Add(17), "Array's capacity must be exactly 16 integers!");
        }


        [TestCase(new int[] { 1, 2 })]
        public void RemoveMethodRemovesCorrectly(int[] addedData)
        {
            database = new(addedData);

            int[] expectedresult = new int[] { 1 };
            database.Remove();
            int[] actualResult = database.Fetch();

            Assert.AreEqual(expectedresult, actualResult);
        }


        [TestCase(new int[] { })]
        public void RemoveMethodThrowsExeptionWhenEmpty(int[] addedData)
        {
            database = new(addedData);

            Assert.Throws<InvalidOperationException>(() => database.Remove(), "The collection is empty!");
        }
    }
}
