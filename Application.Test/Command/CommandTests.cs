using Application;
using Application.Interfaces;
using Application.Services;
using System;
using System.Linq;
using Xunit;
namespace Application.Test
{
    public class CommandTests
    {
        private IProductService productService;
        private IOrderService orderService;
        private ICampaignService campaignService;
        private ICommand command;
        private Command GetCommand()
        {
            productService = new ProductService();
            orderService = new OrderService();
            campaignService = new CampaignService(orderService);
            return new Command(orderService, campaignService, productService);
        }
        [Fact]
        public void  GetCampaignInfo()
        {
            command = GetCommand();
            command.Execute("create_product", new string[] { "p1", "100", "1000" });
            command.Execute("create_campaign", new string[] { "c1", "p1", "10", "20", "100" });
            command.Execute("get_campaign_info", new string[] { "c1", "p1", "10", "20", "100" });
            var campaign = campaignService.GetCampaignInfo("c1");
            Assert.Equal("p1",campaign.Products.ProductCode);
            Assert.Equal(20, campaign.PriceManupulationLimit);
            Assert.Equal(100, campaign.TargetSalesCount);
            Assert.Equal(10, campaign.TimeDuration);


        }
        [Fact]
        public void IsOrderCreated()
        {
            command = GetCommand();
            command.Execute("create_product", new string[] { "p1", "100", "1000" });
            command.Execute("create_order", new string[] { "p1", "10" });
            var order = orderService.GetOrders().FirstOrDefault();

            Assert.Equal("p1", order.Products.ProductCode);
            Assert.Equal(100, order.Products.Price);
            Assert.Equal(990, order.Products.Stock);
            Assert.Equal(10, order.Quantity);

        }
        [Fact]
        public void IsProductCreated()
        {
            command = GetCommand();
            command.Execute("create_product", new string[] { "p2", "100", "1000" });

            var product = productService.GetProducts("p2");


            Assert.Equal(100, product.Price);
            Assert.Equal(1000, product.Stock);

        }
        [Fact]
        public void IsCampingCreated()
        {
            command = GetCommand();

            command.Execute("create_product", new string[] { "p1", "100", "1000" });

            //logs on console
            command.Execute("create_campaign", new string[] { "c1", "p1", "10", "20", "100" });

            var campaign = campaignService.GetCampaignInfo("c1");

            Assert.Equal("p1", campaign.Products.ProductCode);
            Assert.Equal(20, campaign.PriceManupulationLimit);
            Assert.Equal(100, campaign.TargetSalesCount);
            Assert.Equal(10, campaign.TimeDuration);
        }
        [Fact]
        public void GetProductInfo()
        {
            command = GetCommand();
            command.Execute("create_product", new string[] { "p3", "100", "1000" });
            command.Execute("get_product_info", new string[] { "p3" });
            var product = productService.GetProducts("p3");


            Assert.Equal(100, product.Price);
            Assert.Equal(1000, product.Stock);
        }
    }
}
