using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repositoo;
using Orders.Models;

namespace Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton
            (
                (s) => new OrdersRepository
                (
                    new InMemoryOperations<string, Order>(o => o.Id)
                )
            );
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OrdersRepository orders)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            SeedRepository(orders);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SeedRepository(OrdersRepository orders)
        {
            var customer1 = new Buyer { Id = 1, FullName = "John Jackson", ShipingAddress = "PO123456" };
            var customer2 = new Buyer { Id = 2, FullName = "Jim Tailor", ShipingAddress = "PO123456" };

            var order1 = new Order("order1", DateTime.Now, customer1);
            var order2 = new Order("order2", DateTime.Now, customer2);

            order1.AddProduct
            (
                new Item
                {
                  Name = "iPhone",
                  Price = 999.99,
                  ProductNumber = "p1"

                }
            );

            order2.AddProduct
            (
                new Item
                {
                    Name = "Galaxy Note X",
                    Price = 1199.99,
                    ProductNumber = "p2"

                }
            );

            order2.AddProduct
            (
                new Item
                {
                    Name = "iPhone",
                    Price = 999.99,
                    ProductNumber = "p1"

                }
            );

            orders.Add(order1);
            orders.Add(order2);

        }




    }
}
