using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private List<Orders> OrderList { get; set; }

        public OrderService()
        {
            if (OrderList == null)
                OrderList = new List<Orders>();
        }
        public void Create(Products product, int quantity, TimeSpan systemTime)
        {
            if (product.Stock >= quantity)
            {
                product.Stock -= quantity;

                var order = new Orders(product, quantity);


                if (product.Campaigns != null && product.Campaigns.Status)
                {
                    var finishCampaign = product.Campaigns;
                    if (systemTime < TimeSpan.FromHours(finishCampaign.TimeDuration) &&
                        finishCampaign.TotalSalesCount + quantity < finishCampaign.TargetSalesCount)
                    {
                        finishCampaign.TotalSalesCount += quantity;

                        order.Campaigns = finishCampaign;
                        order.Products.Price = product.Price;
                        OrderList.Add(order);
                        Console.WriteLine("Order created; product " + product.ProductCode + ", quantity " + quantity);
                    }

                }
                else
                {
                    order.Products.Price = product.Price;
                    OrderList.Add(order);
                    Console.WriteLine("Order created; product " + product.ProductCode + ", quantity " + quantity);
                }
            }
        }


        public List<Orders> GetOrders()
        {
            return OrderList;
        }
        public int TotalSalesByCampaign(string campaignName)
        {
            return OrderList.Where(x => x.Products.Campaigns != null && x.Products.Campaigns.CampaignName == campaignName).ToList().Sum(x => x.Quantity);
        }

        public double AverageItemPriceByCampaign(string campaignName)
        {
            int totalSales = TotalSalesByCampaign(campaignName);
            double totalPrice = 0;
            for (int i = 0; i < totalSales; i++)
            {
                totalPrice += OrderList.Where(x => x.Products.Campaigns != null && x.Products.Campaigns.CampaignName == campaignName).ToList()?.Sum(x => x.SalesPrice) ?? 0;
            }
            totalPrice = totalPrice / OrderList.Count();
            if (totalSales == 0 || totalPrice == 0)
            {
                return 0;
            }
            double average = totalPrice / totalSales;

            return average;
        }
    }
}
