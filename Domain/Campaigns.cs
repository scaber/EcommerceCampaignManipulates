using System;

namespace Domain
{
    public class Campaigns : BaseEntity
    {
        public Campaigns(string campaignName, Products product, int duration, int priceManipulationLimit, int targetSalesCount)
        {
            Status = true;
            CampaignName = campaignName;
            Products = product;
            TimeDuration =  duration ;
            Id = new Guid();
            TargetSalesCount = targetSalesCount;
            PriceManupulationLimit = priceManipulationLimit ;

        }
        public string CampaignName { get; set; }
        public int ProductId { get; set; }
        public int TimeDuration { get; set; }
        public double Price { get; set; }
        public double DiscountPercent { get; set; }
        public int TargetSalesCount { get; set; }
        public int TotalSalesCount { get; set; }
        public double PriceManupulationLimit { get; set; }
        public bool Status { get; set; }
        public Products Products { get; set; }

    }
}
