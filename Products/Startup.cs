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
using Products.Models;

namespace Products
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
                new ProductRepository(new InMemoryOperations<string, Models.Product>(p => p.ProductNumber))
            );
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductRepository products)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            SeedRepository(products);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SeedRepository(ProductRepository products)
        {
            products.Add
            (
                new Product
                {
                     ProductNumber = "p1",
                     Name = "iPhone X",
                     Manufacturer = "Apple",
                     Price =  999.99
                }
            );

            products.Add
            (
                new Product
                {
                    ProductNumber = "p2",
                    Name = "Galaxy Note X",
                    Manufacturer = "Samsung",
                    Price = 1199.99
                }
            );

        }
    }
}
