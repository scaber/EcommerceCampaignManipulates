using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class Orders: BaseEntity
    {
        public Orders(Products products,int quantity)
        {
            Products = products;
            Quantity = quantity;
            SalesPrice = products.Price;
            Campaigns = products.Campaigns;
        }
        public string ProductCode { get; set; } 
        public int Quantity { get; set; }
        public Products Products { get; set; }
        public Campaigns Campaigns { get; set; }
        public double SalesPrice { get; set; }

        public object FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
