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
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Repositoo;
using Orders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            services.AddHealthChecks()
                .AddCheck<OrdersHealthChecks>("Orders API")
                .AddCheck<OrdersRepositoryHealthChecks>("Orders Repository");


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
                //endpoints.MapHealthChecks("/hc");
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                     ResponseWriter = HealthCheckResponse
                });
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

        private Task HealthCheckResponse(HttpContext httpContext, HealthReport healthReport)
        {

            JArray result = new JArray();

            foreach (var e in healthReport.Entries)
            {
                var jObj = new JObject();
                jObj.Add("serviceName", e.Key);
                jObj.Add("state", e.Value.Status.ToString());
                result.Add(jObj);


            }
            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));


        }




    }
}
