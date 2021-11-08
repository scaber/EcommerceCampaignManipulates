using Domain;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        void Create(Products product, int quantity, TimeSpan systemTime);
        List<Orders> GetOrders(); 
        int TotalSalesByCampaign(string campaignName);
        double AverageItemPriceByCampaign(string campaignName);
    }
}
