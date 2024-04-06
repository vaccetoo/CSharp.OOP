using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace SocialMediaManager.Tests
{
    public class Tests
    {
        [Test]
        public void CreateInfluencerRepositoryConstructorWorksCorrect()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();

            Assert.IsNotNull(influencerRepository);
            Assert.AreEqual(0, influencerRepository.Influencers.Count);
        }

        [Test]
        public void RegisterInfluencerThrowsException()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();
            Influencer influencer = null;

            Assert.Throws<ArgumentNullException>(() => influencerRepository.RegisterInfluencer(influencer), "Influencer is null");
        }

        [Test]
        public void RegisterThrowsExceptionIfIsAlreadyregistered()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();

            Influencer influencer1 = new Influencer("Influencer1", 1);
            Influencer influencer2 = new Influencer("Influencer1", 2);

            influencerRepository.RegisterInfluencer(influencer1);

            Assert.Throws<InvalidOperationException>(() => influencerRepository.RegisterInfluencer(influencer2));
        }

        [Test]
        public void RegisterWorksCorrect()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();

            Influencer influencer1 = new Influencer("Influencer1", 1);
            Influencer influencer2 = new Influencer("Influencer2", 2);

            string result = influencerRepository.RegisterInfluencer(influencer1);

            influencerRepository.RegisterInfluencer(influencer2);

            Assert.AreEqual("Successfully added influencer Influencer1 with 1", result);
            Assert.AreEqual(2, influencerRepository.Influencers.Count);
        }

        [TestCase(null)]
        [TestCase("     ")]
        public void RemoveInfluencerArgNullException(string name)
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();

            Influencer influencer1 = new Influencer("Influencer1", 1);
            influencerRepository.RegisterInfluencer(influencer1);

            Assert.Throws<ArgumentNullException>(() => influencerRepository.RemoveInfluencer(name));
        }

        [TestCase("Influencer1")]
        public void RemoveWorksCorrect(string name)
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();

            Influencer influencer1 = new Influencer("Influencer1", 1);
            influencerRepository.RegisterInfluencer(influencer1);

            bool result = influencerRepository.RemoveInfluencer(name);

            Assert.IsTrue(result);
            Assert.AreEqual(0, influencerRepository.Influencers.Count);
        }

        [Test]
        public void GetInfluencerWithMostFollowers()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();

            Influencer influencer1 = new Influencer("Influencer1", 1);
            Influencer influencer2 = new Influencer("Influencer2", 2);
            Influencer influencer3 = new Influencer("Influencer3", 3);
            Influencer influencer4 = new Influencer("Influencer4", 4);

            influencerRepository.RegisterInfluencer(influencer1);
            influencerRepository.RegisterInfluencer(influencer2);
            influencerRepository.RegisterInfluencer(influencer3);
            influencerRepository.RegisterInfluencer(influencer4);

            Influencer result = influencerRepository.GetInfluencerWithMostFollowers();

            Assert.AreEqual(influencer4, result);
        }

        [Test]
        public void GetInfluencerWorksCorrect()
        {
            InfluencerRepository influencerRepository = new InfluencerRepository();

            Influencer influencer1 = new Influencer("Influencer1", 1);
            Influencer influencer2 = new Influencer("Influencer2", 2);
            Influencer influencer3 = new Influencer("Influencer3", 3);
            Influencer influencer4 = new Influencer("Influencer4", 4);

            influencerRepository.RegisterInfluencer(influencer1);
            influencerRepository.RegisterInfluencer(influencer2);
            influencerRepository.RegisterInfluencer(influencer3);
            influencerRepository.RegisterInfluencer(influencer4);

            Influencer result = influencerRepository.GetInfluencer("Influencer2");

            Assert.AreEqual(influencer2, result);
        }

    }
}