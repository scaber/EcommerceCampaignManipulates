using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICampaignService
    {
        void Create(string campaignName, Products product, int duration, int priceManipulationLimit, int targetSalesCount);

        Campaigns GetCampaignInfo(string name);

     }
}
