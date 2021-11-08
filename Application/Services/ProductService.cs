using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private List<Products> ProductsList { get; set; }
        public ProductService()
        {
            if (ProductsList == null)
                ProductsList = new List<Products>();
        }
        public void ChangeProductPrice(string productCode, double price)
        {
            var product = ProductsList.Where(x => x.ProductCode == productCode).FirstOrDefault();
            if (product != null)
            {
                if (product.Price>0 && product.RealPrice> 0)
                {
                    product.Price = price;
                    product.RealPrice = price;
                }
                else
                {
                    Console.WriteLine("Price should be greater or equal to zero");
                }
  
            }
        }

        public void Create(string productCode, double price, int stock)
        {
          
            if (ProductsList.Any(x=>x.ProductCode==productCode))
            {
                Console.WriteLine("This product already exists");
            }
            else
            {
                ProductsList.Add(new Products(productCode, price, stock));

                Console.WriteLine("Product created; code "+productCode+", price  "+price+", stock "+stock);
            }
        }

        public Products GetProducts(string productCode)
        {
            var product = ProductsList.Where(x =>x.ProductCode!=null && x.ProductCode == productCode).FirstOrDefault();
            if (product != null)
            {
                return product;
            }
            else
            {
                Console.WriteLine("Product not exits");
                return null;
            }
        }

        public void IncraseTime(int totalIncrase)
        {
            for (int i = 0; i < totalIncrase; i++)
            {
                foreach (var item in ProductsList)
                {
                    MakeDiscounted(item, -5);
                }
            } 
        }

        private void MakeDiscounted(Products item, double discountedPrice)
        {
            if (item.Campaigns.Status)
            {
                item.Price = item.Price + discountedPrice;
                if (item.Price <item.RealPrice*(100-item.Campaigns.PriceManupulationLimit)/100)
                {
                    item.Price = item.RealPrice;
                    item.Campaigns.Status = false;
                }
            }
        }
    }
}
