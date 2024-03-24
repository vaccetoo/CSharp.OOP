namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static System.Collections.Specialized.BitVector32;

    public class Tests
    {
        private RailwayStation railwaystation;

        [SetUp]
        public void SetUp()
        {
            railwaystation = new("Station");

            railwaystation.NewArrivalOnBoard("Arrival");
        }

        [Test]
        public void ConstructorAcceptCorrectParameters()
        {
            RailwayStation station = new("Station");

            string expextedStationName = station.Name;
            Queue<string> expextedArrivalTrains = new Queue<string>();
            Queue<string> expextedDepartureTrains = new Queue<string>();

            Assert.AreEqual(expextedStationName, station.Name);
            Assert.AreEqual(expextedArrivalTrains, station.ArrivalTrains);
            Assert.AreEqual(expextedDepartureTrains, station.DepartureTrains);
        }

        [TestCase(null)]
        [TestCase(" ")]
        public void NameSetterShouldthrowExceptionIfNullOrWhiteSpace(string value) 
        {
            Assert.Throws<ArgumentException>(() => new RailwayStation(value), "Name cannot be null or empty!");
        }

        [Test]
        public void NameGetterWorksCorrect()
        {
            string expectedResult = "Station";
            string actualResult = railwaystation.Name;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ArrivalTrainsReturnCorrectTrains()
        {
            Queue<string> expctedArrivals = new Queue<string>();
            expctedArrivals.Enqueue("Arrival");

            Queue<string> actualResultArrivals = railwaystation.ArrivalTrains;

            Assert.AreEqual(expctedArrivals, actualResultArrivals);
        }


        [Test]
        public void DeparturetrainsReturnCorrectTrains()
        {
            railwaystation.TrainHasArrived("Arrival");

            Queue<string> expectedresult = new Queue<string>();
            expectedresult.Enqueue("Arrival");

            Queue<string> actualResult = railwaystation.DepartureTrains;

            Assert.AreEqual(expectedresult, actualResult);
        }


        [TestCase("Arrival2")]
        [TestCase("Arrival3")]
        public void NewArrivalOnBoardSetsCorrectNewArrival(string trainInfo)
        {
            railwaystation.NewArrivalOnBoard(trainInfo);

            string expectedresult = trainInfo;
            string actualResult = railwaystation.ArrivalTrains.Last();

            Assert.AreEqual(expectedresult, actualResult);
        }


        [TestCase("Arrival1")]
        public void TrainHasArrivedReturnCorrectMessageIfTrainIsNotInQueue(string trainInfo)
        {
            string expectedResult = $"There are other trains to arrive before {trainInfo}.";
            string actualresult = railwaystation.TrainHasArrived(trainInfo);

            Assert.AreEqual(expectedResult, actualresult);
        }


        [TestCase("Arrival")]
        public void TrainHasArrivedReturnCorrectMessageIfTrainIsInQueue(string trainInfo)
        {
            string expectedResult = $"{trainInfo} is on the platform and will leave in 5 minutes.";
            string actualresult = railwaystation.TrainHasArrived(trainInfo);

            Assert.AreEqual(expectedResult, actualresult);
        }


        [Test]
        public void TrainHasLeftReturnTrueIfTrainIsInDepartures()
        {
            railwaystation.TrainHasArrived("Arrival");

            bool expectedResult = true;
            bool actualResult = railwaystation.TrainHasLeft("Arrival");

            Assert.AreEqual(expectedResult, actualResult);
        }


        [Test]
        public void TrainHasLeftReturnFalseIfTrainIsNotInDepartures()
        {
            railwaystation.TrainHasArrived("Arrival");

            bool expectedResult = false;
            bool actualResult = railwaystation.TrainHasLeft("Arrival1");

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}