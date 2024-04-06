using InfluencerManagerApp.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public abstract class Influencer : IInfluencer
    {
        private string username;
        private int followers;
        private double engagementRate;
        private List<string> participations;

        protected Influencer(string username, int followers, double engagementRate)
        {
            Username = username;
            Followers = followers;
            EngagementRate = engagementRate;

            Income = 0;
            participations = new List<string>();
        }

        public string Username
        {
            get => username; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username is required.");
                }
                username = value;
            }
        }

        public int Followers
        {
            get => followers; 
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Followers count cannot be a negative number.");
                }
                followers = value;
            }
        }

        public double EngagementRate 
        { 
            get => engagementRate; 
            private set => engagementRate = value; 
        }

        public double Income { get; private set; }

        public IReadOnlyCollection<string> Participations
            => participations;

        public abstract int CalculateCampaignPrice();

        public void EarnFee(double amount)
        {
            Income += amount;
        }

        public void EndParticipation(string brand)
        {
            participations.Remove(brand);
        }

        public void EnrollCampaign(string brand)
        {
            participations.Add(brand);
        }

        public override string ToString()
            => $"{Username} - Followers: {Followers}, Total Income: {Income}";
    }
}
