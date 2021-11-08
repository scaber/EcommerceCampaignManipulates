using Application;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                                        .AddSingleton<IOrderService, OrderService>() 
                                        .AddSingleton<IProductService,ProductService>()
                                        .AddSingleton<ICampaignService, CampaignService>()
                                        .AddSingleton<ICommand,Command>()
                                        .BuildServiceProvider();


            var commandParser = (ICommand)serviceProvider.GetService(typeof(ICommand));
        make:
            var input = Console.ReadLine().Split(' '); 
            commandParser.Execute(input[0], input[1..]);

            goto make;
        }


    }
}
