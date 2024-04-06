using InfluencerManagerApp.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public abstract class Campaign : ICampaign
    {
        private string brand;
        private List<string> contributorsNames;

        protected Campaign(string brand, double budget)
        {
            Brand = brand;
            Budget = budget;

            contributorsNames = new List<string>();
        }

        public string Brand
        {
            get => brand; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Brand is required.");
                }
                brand = value;
            }
        }

        public double Budget { get; private set; }

        public IReadOnlyCollection<string> Contributors 
            => contributorsNames;

        public void Engage(IInfluencer influencer)
        {
            contributorsNames.Add(influencer.Username);

            Budget -= influencer.CalculateCampaignPrice();
        }

        public void Gain(double amount)
        {
            Budget += amount;
        }

        public override string ToString()
            => $"{GetType().Name} - Brand: {Brand}, Budget: {Budget}, Contributors: {contributorsNames.Count()}";
    }
}
