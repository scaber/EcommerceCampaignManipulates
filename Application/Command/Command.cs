using Application.Interfaces;
using System;
using System.Collections.Generic;

namespace Application
{
    public class Command : ICommand
    {
        private static Dictionary<string, Action<string[]>> CommandList;
        private TimeSpan systemTime;
        private readonly IProductService productService;
        private readonly ICampaignService campaignService;
        private readonly IOrderService orderService;
        public Command(IOrderService orderService, ICampaignService campaignService, IProductService productService)
        {
            this.productService = productService;
            this.campaignService = campaignService;
            this.orderService = orderService;
            systemTime = new TimeSpan(0, 0, 0);
            Init();
        }
        public void Execute(string command, string[] args)
        {

            if (CommandList.ContainsKey(command))
            {
                CommandList[command].Invoke(args);
            }
            else
            {
                Console.WriteLine("Command is not found.");
            }
        }

        public TimeSpan GetTime()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            if (CommandList == null)
            {
                CommandList = new Dictionary<string, Action<string[]>>();

                CommandList.Add("create_product", CreateProduct);
                CommandList.Add("create_campaign", CreateCampaign);
                CommandList.Add("increase_time", IncraseTime);
                CommandList.Add("change_product_price", ChangeProductPrice);
                CommandList.Add("get_product_info", GetProductInfo);
                CommandList.Add("create_order", CreateOrder);
                CommandList.Add("get_campaign_info", GetCampaignInfo);

            }
        }
        public void GetCampaignInfo(string[] args)
        {
            string campaignName = GetParameter<string>(args, 0);

            campaignService.GetCampaignInfo(campaignName);
        }
        public void IncraseTime(string[] args)
        {
            int totalIncrease = Convert.ToInt32(args[0]);
            systemTime = systemTime.Add(new TimeSpan(totalIncrease, 0, 0));
            Console.WriteLine("Time is " + systemTime.ToString("hh\\:mm"));
            productService.IncraseTime(totalIncrease);


        }
        public void CreateCampaign(string[] arguments)
        {
            string campaignName = GetParameter<string>(arguments, 0);
            string productCode = GetParameter<string>(arguments, 1);
            int duration = GetParameter<int>(arguments, 2);
            int priceManipulationLimit = GetParameter<int>(arguments, 3);
            int targetSalesCount = GetParameter<int>(arguments, 4);

            var product = productService.GetProducts(productCode);

            campaignService.Create(campaignName, product, duration, priceManipulationLimit, targetSalesCount);
        }
        public void GetProductInfo(string[] arguments)
        {
            string productCode = GetParameter<string>(arguments, 0);

            var product = productService.GetProducts(productCode);
            if (product != null)
            {
                Console.WriteLine($"Product {product.ProductCode} info; price {product.Price}, stock {product.Stock}");
            }
            else
            {
                Console.WriteLine($"Product {productCode} not found"); 
            } 
        }
        private void ChangeProductPrice(string[] arguments)
        {
            string productCode = GetParameter<string>(arguments, 0);
            double price = GetParameter<double>(arguments, 1);
            productService.ChangeProductPrice(productCode, price);
        }
        private void CreateProduct(string[] arguments)
        {
            string productCode = GetParameter<string>(arguments, 0);
            double price = GetParameter<double>(arguments, 1);
            int stock = GetParameter<int>(arguments, 2);

            productService.Create(productCode, price, stock);


        }
        private T GetParameter<T>(string[] values, int index)
        {
            try
            {
                return (T)Convert.ChangeType(values[index], typeof(T));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexcepted paramater value.");
                return Activator.CreateInstance<T>();
            }
        }
        public void CreateOrder(string[] args)
        {
            string productCode = GetParameter<string>(args, 0);
            int quantity = GetParameter<int>(args, 1);
            var product = productService.GetProducts(productCode);

            orderService.Create(product, quantity, systemTime);
        }
    }
}
