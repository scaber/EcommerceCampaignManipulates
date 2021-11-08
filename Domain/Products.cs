using System;

namespace Domain
{
    public class Products: BaseEntity
    {
        public Products(string productCode,double price,int stock
            )
        {
            ProductCode = productCode;
            Price = price;
            Stock = stock;
            RealPrice = price;
            Id = new Guid();

        }
        public string ProductCode { get; set; }
        public double  RealPrice { get; set; }
        public double  Price { get; set; }
        public int   Stock { get; set; }
        public Campaigns Campaigns { get; set; }
        

    }
}
