namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Reflection.PortableExecutable;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;


        [SetUp]
        public void SetUp()
        {
            Person[] people =
          {
                new Person(1, "user1"),
                new Person(2, "user2"),
                new Person(3, "user3"),
                new Person(4, "user4"),
                new Person(5, "user5"),
                new Person(6, "user6"),
                new Person(7, "user7"),
                new Person(8, "user8"),
                new Person(9, "user9"),
                new Person(10, "user10"),
                new Person(11, "user11"),
                new Person(12, "user12"),
                new Person(13, "user13"),
                new Person(14, "user14"),
                new Person(15, "user15")
            };

            database = new(people);
        }



        [Test]
        public void If_Created_With_More_Than_16_Throw_Exeption()
        {
            Person[] people =
         {
                new Person(1, "user1"),
                new Person(2, "user2"),
                new Person(3, "user3"),
                new Person(4, "user4"),
                new Person(5, "user5"),
                new Person(6, "user6"),
                new Person(7, "user7"),
                new Person(8, "user8"),
                new Person(9, "user9"),
                new Person(10, "user10"),
                new Person(11, "user11"),
                new Person(12, "user12"),
                new Person(13, "user13"),
                new Person(14, "user14"),
                new Person(15, "user15"),
                new Person(16, "user16"),
                new Person(17, "user17")
            };

            Assert.Throws<ArgumentException>(() => database = new(people), "Provided data length should be in range [0..16]!");

        }


        [Test]
        public void Test_Count_Getter()
        {
            int expectedCount = 15;
            int actualCount = database.Count;

            Assert.AreEqual(expectedCount, actualCount);

        }



        [Test]
        public void Test_Add_Method_DataBaseCapacity_Throw_Exeption()
        {
            Person[] people =
            {
                new Person(1, "user1"),
                new Person(2, "user2"),
                new Person(3, "user3"),
                new Person(4, "user4"),
                new Person(5, "user5"),
                new Person(6, "user6"),
                new Person(7, "user7"),
                new Person(8, "user8"),
                new Person(9, "user9"),
                new Person(10, "user10"),
                new Person(11, "user11"),
                new Person(12, "user12"),
                new Person(13, "user13"),
                new Person(14, "user14"),
                new Person(15, "user15"),
                new Person(16, "user16")
            };

            database = new(people);

            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(17, "user17")), "Array's capacity must be exactly 16 integers!");
        }


        [Test]
        public void Test_Add_Method_UserNameExist_Throw_Exeption()
        {
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(100, "user1")), "There is already user with this username!");
        }


        [Test]
        public void Test_Add_Method_IdExist_Exeption()
        {
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(1, "anyName")), "There is already user with this Id!");
        }


        [Test]
        public void Test_Remove_Method_Count_Should_Be_Decreased()
        {
            int expectedResult = 14;

            database.Remove();
            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }


        [Test]
        public void Test_Remove_Method_If_Count_Zero_Throw_Exeption()
        {
            database = new(new Person[0]);

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }


      
        [TestCase(null)]
        [TestCase("")]
        public void Test_FindByUserName_IfNullOrEmpty_Throw_Exeption(string name)
        {
            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(name), "Username parameter is null!");
        }


        [Test]
        public void FindByUsername_Method_Should_Be_Case_Sensitive()
        {
            string expectedResult = "UseR1";
            string actualResult = database.FindByUsername("user1").UserName;

            Assert.AreNotEqual(expectedResult, actualResult);
        }



        [Test]
        public void Test_FindByUserName_IfNameDoesNotExist_Throw_Exeption()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("ThisUserDoesNotExist"), "No user is present by this username!");
        }


        [TestCase(-1)]
        [TestCase(-20)]
        public void Test_FindById_If_Id_Less_Than_Zero_Throw_Exeption(long invalidNumber)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(invalidNumber), "Id should be a positive number!");
        }


        [TestCase(123)]
        [TestCase(1234)]
        public void Test_FindById_If_Id_DoesNot_Exist_Throw_Exeption(long invalidId)
        {
            Assert.Throws<InvalidOperationException>(() => database.FindById(invalidId), "No user is present by this ID!");
        }
    }
}