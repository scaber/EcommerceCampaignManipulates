using Application.Interfaces;
using Application.Services;
using Xunit;

namespace Application.Test.Services
{
    public class CampaignServiceTests
    {
        private readonly ICampaignService campaignService;
        private readonly IProductService productService;

        public CampaignServiceTests()
        {
            campaignService =  new CampaignService(new OrderService());
            productService = new ProductService();
        }
        [Fact]
        public void Campaign_Added()
        {
            productService.Create("p1", 100, 1000);
            var product = productService.GetProducts("p1"); 
            campaignService.Create("c1", product, 10, 20, 100); 
            var campaign = campaignService.GetCampaignInfo("c1"); 
            Assert.Equal(100, campaign.TargetSalesCount);
            Assert.Equal(20, campaign.PriceManupulationLimit);
            Assert.Equal(10, campaign.TimeDuration);
            Assert.Equal("c1", campaign.CampaignName);
            Assert.Equal("p1",campaign.Products.ProductCode);

        }
    }
}
