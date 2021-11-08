using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class CampaignService : ICampaignService
    {
        private List<Campaigns> CampaignList { get; set; }
        private readonly IOrderService _orderService;

        public CampaignService(IOrderService orderService)
        {
            if (CampaignList == null)
                CampaignList = new List<Campaigns>();
            _orderService = orderService;
        }
        public void Create(string campaignName, Products product, int duration, int priceManipulationLimit, int targetSalesCount)
        {
            if (CampaignList.Where(x => x.CampaignName == campaignName).FirstOrDefault() == null)
            {
                if (!(product.Campaigns != null ? product.Campaigns.Status : false) && product.Stock - targetSalesCount >= 0)
                {

                    var campaign = new Campaigns(campaignName, product, duration, priceManipulationLimit, targetSalesCount);

                    product.Campaigns = campaign;

                    CampaignList.Add(campaign);

                    Console.WriteLine("Campaign created; name " + campaign.CampaignName + ", product " + product.ProductCode + ", duration  " + campaign.TimeDuration + ", limit " + campaign.PriceManupulationLimit + ", target sales count " + campaign.TargetSalesCount);
                }
            }
            else
            {
                throw new Exception("Campaign name already exists");
            }
        }

        public Campaigns GetCampaignInfo(string name)
        {
            Campaigns campaign = CampaignList.Where(x => x.CampaignName == name).FirstOrDefault();
            if (campaign != null)
            {
                var anyOrder = _orderService.GetOrders().Count() > 0 ? true : false;
                double totalSales = 0;
                double avarageItemPrice = 0;

                if (anyOrder)
                {
                    avarageItemPrice = _orderService.AverageItemPriceByCampaign(campaign.CampaignName);
                    totalSales = campaign.TargetSalesCount / campaign.PriceManupulationLimit * campaign.TimeDuration;
                }

                Console.WriteLine($"Campaign {campaign.CampaignName} info; Status {(campaign.Status == false ? "Ended" : "Active")}, Target Sales {campaign.TargetSalesCount}, Total Sales {totalSales}, Turnover {(anyOrder == true ? totalSales * avarageItemPrice : 0)}, Average Item Price {(anyOrder == true ? avarageItemPrice : "-")}");
            }
            return campaign;
        }
    }
}
