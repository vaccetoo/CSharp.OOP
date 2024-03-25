namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static System.Collections.Specialized.BitVector32;

    [TestFixture]
    public class RaiwayStationTests
    {
        [Test]
        public void ConstructorWorksCorrect()
        {
            RailwayStation railwayStation = new("Station");

            Assert.AreEqual("Station", railwayStation.Name);
            Assert.AreEqual(0, railwayStation.ArrivalTrains.Count);
            Assert.AreEqual(0, railwayStation.DepartureTrains.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        public void ConstructorThrowsExceptionIfNameIsNullOrWhiteSpace(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() 
                => new RailwayStation(name));

            Assert.AreEqual("Name cannot be null or empty!", exception.Message);
        }

        [Test]
        public void NewArrivalOnBoardAddTrainCorrect()
        {
            RailwayStation railwayStation = new("Station");

            railwayStation.NewArrivalOnBoard("Train");

            Assert.AreEqual(1, railwayStation.ArrivalTrains.Count);
        }

        [Test]
        public void TrainHasArrivedWorksCorrect()
        {
            RailwayStation railwayStation = new("Station");

            railwayStation.NewArrivalOnBoard("Arrival");

            string result = railwayStation.TrainHasArrived("Arrival");

            Assert.AreEqual("Arrival is on the platform and will leave in 5 minutes.", result);

            Assert.AreEqual(1, railwayStation.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasArrivedReturnMessageIfTheTrainIsNotFirst()
        {
            RailwayStation railwayStation = new("Station");

            railwayStation.NewArrivalOnBoard("Arrival1");
            railwayStation.NewArrivalOnBoard("Arrival2");
            railwayStation.NewArrivalOnBoard("Arrival3");

            string result = railwayStation.TrainHasArrived("Arrival");

            Assert.AreEqual("There are other trains to arrive before Arrival.", result);
        }

        [Test]
        public void TrainHasLeftReturnTrue()
        {
            RailwayStation railwayStation = new("Station");

            railwayStation.NewArrivalOnBoard("Arrival");
            railwayStation.TrainHasArrived("Arrival");

            bool result = railwayStation.TrainHasLeft("Arrival");

            Assert.IsTrue(result);
            Assert.AreEqual(0, railwayStation.DepartureTrains.Count);

        }

        [Test]
        public void TrainHasLeftReturnFalse()
        {
            RailwayStation railwayStation = new("Station");

            railwayStation.NewArrivalOnBoard("Arrival");
            railwayStation.TrainHasArrived("Arrival");

            bool result = railwayStation.TrainHasLeft("Arrival1");

            Assert.IsFalse(result);
        }
    }
}