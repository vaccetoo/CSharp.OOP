using InfluencerManagerApp.Core.Contracts;
using InfluencerManagerApp.Models;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories;
using InfluencerManagerApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Core
{
    public class Controller : IController
    {
        private IRepository<IInfluencer> influencers;
        private IRepository<ICampaign> campaigns;

        public Controller()
        {
            influencers = new InfluencerRepository();
            campaigns = new CampaignRepository();
        }

        public string ApplicationReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var influencer in influencers
                .Models
                .OrderByDescending(i => i.Income)
                .ThenByDescending(i => i.Followers))
            {
                sb.AppendLine(influencer.ToString());

                if (influencer.Participations.Any())
                {
                    sb.AppendLine("Active Campaigns:");

                    foreach (var campaignName in influencer.Participations.OrderBy(name => name))
                    {
                        ICampaign currentCampaign = campaigns.FindByName(campaignName);

                        sb.AppendLine($"--{currentCampaign.ToString()}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string AttractInfluencer(string brand, string username)
        {
            if (influencers.FindByName(username) == null)
            {
                return $"{nameof(InfluencerRepository)} has no {username} registered in the application.";
            }

            if (campaigns.FindByName(brand) == null)
            {
                return $"There is no campaign from {brand} in the application.";
            }

            IInfluencer influencer = influencers.FindByName(username);

            if (influencer.Participations.Contains(brand))
            {
                return $"{username} is already engaged for the {brand} campaign.";
            }

            ICampaign campaign = campaigns.FindByName(brand);

            bool isCorrectForCampaign = false;

            if (campaign.GetType().Name == nameof(ProductCampaign) && (influencer.GetType().Name == nameof(BusinessInfluencer) || influencer.GetType().Name == nameof(FashionInfluencer)))
            {
                isCorrectForCampaign = true;
            }
            else if (campaign.GetType().Name == nameof(ServiceCampaign) && (influencer.GetType().Name == nameof(BusinessInfluencer) || influencer.GetType().Name == nameof(BloggerInfluencer)))
            {
                isCorrectForCampaign = true;
            }
            else
            {
                return $"{username} is not eligible for the {brand} campaign.";
            }


            if (campaign.Budget < influencer.CalculateCampaignPrice())
            {
                return $"The budget for {brand} is insufficient to engage {username}.";
            }
            else
            {
                influencer.EnrollCampaign(campaign.Brand);
                influencer.EarnFee(influencer.CalculateCampaignPrice());

                campaign.Engage(influencer);

                return $"{username} has been successfully attracted to the {brand} campaign.";
            }
        }

        public string BeginCampaign(string typeName, string brand)
        {
            if (typeName != nameof(ProductCampaign) &&
                typeName != nameof(ServiceCampaign))
            {
                return $"{typeName} is not a valid campaign in the application.";
            }

            if (campaigns.FindByName(brand) != null)
            {
                return $"{brand} campaign cannot be duplicated.";
            }

            ICampaign campaign = typeName switch
            {
                "ProductCampaign" => new ProductCampaign(brand),
                "ServiceCampaign" => new ServiceCampaign(brand)
            };

            campaigns.AddModel(campaign);

            return $"{brand} started a {typeName}.";
        }

        public string CloseCampaign(string brand)
        {
            if (campaigns.FindByName(brand) == null)
            {
                return $"Trying to close an invalid campaign.";
            }

            ICampaign campaign = campaigns.FindByName(brand);

            if (campaign.Budget <= 10_000)
            {
                return $"{brand} campaign cannot be closed as it has not met its financial targets.";
            }

            foreach (var influencer in influencers.Models)
            {
                if (influencer.Participations.Contains(campaign.Brand))
                {
                    influencer.EarnFee(2_000);
                    influencer.EndParticipation(campaign.Brand);
                }
            }

            campaigns.RemoveModel(campaign);

            return $"{brand} campaign has reached its target.";
        }

        public string ConcludeAppContract(string username)
        {
            if (influencers.FindByName(username) == null)
            {
                return $"{username} has still not signed a contract.";
            }

            IInfluencer influencer = influencers.FindByName(username);

            if (influencer.Participations.Any())
            {
                return $"{username} cannot conclude the contract while enrolled in active campaigns.";
            }

            influencers.RemoveModel(influencer);

            return $"{username} concluded their contract.";
        }

        public string FundCampaign(string brand, double amount)
        {
            if (campaigns.FindByName(brand) == null)
            {
                return $"Trying to fund an invalid campaign.";
            }

            // TODO: amount <= 0 ???
            if (amount <= 0)
            {
                return $"Funding amount must be greater than zero.";
            }

            ICampaign campaign = campaigns.FindByName(brand);

            campaign.Gain(amount);

            return $"{brand} campaign has been successfully funded with {amount} $";
        }

        public string RegisterInfluencer(string typeName, string username, int followers)
        {
            if (typeName != nameof(BusinessInfluencer) &&
                typeName != nameof(FashionInfluencer) &&
                typeName != nameof(BloggerInfluencer))
            {
                return $"{typeName} has not passed validation.";
            }

            if (influencers.FindByName(username) != null)
            {
                return $"{username} is already registered in {nameof(InfluencerRepository)}.";
            }

            IInfluencer influencer = typeName switch
            {
                "BusinessInfluencer" => new BusinessInfluencer(username, followers),
                "FashionInfluencer" => new FashionInfluencer(username, followers),
                "BloggerInfluencer" => new BloggerInfluencer(username, followers)
            };

            influencers.AddModel(influencer);

            return $"{username} registered successfully to the application.";
        }
    }
}
