using Application.Interfaces;
using Application.Services;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Services
{
    public class OrderServiceTests
    {
        private readonly IOrderService orderService;
        public OrderServiceTests()
        {
            orderService = new OrderService();
        }
        [Fact] 
        public void Order_Added()
        {
            var product = new Products("p1", 100, 1000);

            orderService.Create(product, 5, new TimeSpan(0, 0, 0));

            var order = orderService.GetOrders().Where(x => x.Products.Id == product.Id).FirstOrDefault();
            Assert.Equal(product, order.Products);

        }
        [Fact]
        public void Order_Calculate_Total_SalesByCampaign()
        {
            var product = new Products("p1", 100, 1000);

            product.Campaigns = new Campaigns("c1", product, 10, 20, 10);

            orderService.Create(product, 2, new TimeSpan(0, 0, 0));

            int totalSales = orderService.TotalSalesByCampaign("c1");
            Assert.Equal(2, totalSales);

        }
        [Fact]
        public void Order_Calculate_Average_SalesByCampaign()
        {
            var product = new Products("p1", 100, 1000);

            product.Campaigns = new Campaigns("c1", product, 10, 20, 10);


            orderService.Create(product, 5, new TimeSpan(0, 0, 0));

            double avarageItemPrice = orderService.AverageItemPriceByCampaign("c1");

            Assert.Equal(20, avarageItemPrice);

        }
    }
}
