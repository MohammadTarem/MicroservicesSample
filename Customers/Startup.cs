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
using Customers.Models;
using Repositoo;

namespace Customers
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
                (s) => new CustomerRepository
                (
                    new InMemoryOperations<int, Models.Customer> ( c => c.Id)
                )
            );

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CustomerRepository customers)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            SeedRepository(customers);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SeedRepository(CustomerRepository customers)
        {
            customers.Add
            (
                new Customer
                {
                    Id = 1,
                    Name = "John",
                    Lastname = "Jackson",
                    Address =
                    new Address
                    {
                        Country = "USA",
                        City = "NY",
                        Description = "PO123456"
                    }
                }
            );

            customers.Add
            (
                new Customer
                {
                    Id = 2,
                    Name = "Jim",
                    Lastname = "Tailor",
                    Address =
                    new Address
                    {
                        Country = "USA",
                        City = "Detroit",
                        Description = "PO654321"
                    }
                }
            );


        }
    }
}
