using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public class FashionInfluencer : Influencer
    {
        private const double InitialEngagementRate = 4.0;
        private const double factor = 0.1;

        public FashionInfluencer(string username, int followers) 
            : base(username, followers, InitialEngagementRate)
        {
        }

        public override int CalculateCampaignPrice()
            => (int)Math.Floor(Followers * EngagementRate * factor);
    }
}
