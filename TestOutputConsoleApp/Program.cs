using SupermarketApi.Abstractions.Contexts;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupermarketApi.DataAccess;
using SupermarketApi.Entities;
using System.Configuration;
using System.Collections.Generic;

namespace TestOutputConsoleApp
{
    class Program
    {
        public interface ITester
        {
            
        }
        class TestService : ITester
        {
            public IOrderDbContext orderContext;
            public IOrderProductsDbContext opContext;
            public IProductDbContext productContext;
            public TestService(IOrderDbContext orderContext, IOrderProductsDbContext opContext, IProductDbContext productContext)
            {
                this.orderContext = orderContext;
                this.opContext = opContext;
                this.productContext = productContext;
            }
        }
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<SupermarketDbContext>(options =>
                {
                    options.UseMySQL("Server=127.0.0.1;Port=3306;Database=supermarket;Uid=dotnetTester;Pwd=12345;");
                })
                .AddSingleton<IOrderDbContext, OrderDbContext>()
                .AddSingleton<IOrderProductsDbContext, OrderProductsDbContext>()
                .AddSingleton<IProductDbContext, ProductDbContext>()
                .AddSingleton<ITester, TestService>()
                .BuildServiceProvider();
            TestService testService = (TestService)serviceProvider.GetService<ITester>();
            //OrderProducts opr = new OrderProducts();
            Order order = new Order();
            order.OrderId = 4;
            order.Address ="prueba de update";
            order.Date = DateTime.Now;
            order.TotalPrice = (float)1500.0;
            //testService.orderContext.AddWithProductAsync(order, new int[] {1,10});
            //opr.OrderId = 1;
            //testService.opContext.Remove(opr);
            //Console.WriteLine(testService.orderContext.ListAsync().ToString());
            //opr.OrderId = 1;
            //opr.ProductId = 10;
            //testService.opContext.AddAsync(opr);
            Console.WriteLine("Hello World!");
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(7);
            testService.orderContext.UpdateWithProductsAsync(order, list);
        }
    }
}
