using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace Shoppings
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

            services.AddHttpClient<Services.CustomersService>()
                //.AddPolicyHandler(GetRandomRetryPolicy())
                .AddPolicyHandler(GetConstantRetryPolicy())
                .AddPolicyHandler(GetCircutBreakerPolicy());


            services.AddHttpClient<Services.ProductsService>();
            services.AddHttpClient<Services.OrdersService>();

            


        }

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public static IAsyncPolicy<HttpResponseMessage> GetConstantRetryPolicy()
        {
            
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(5,
                    attempt => TimeSpan.FromSeconds(1) * (attempt / 2),
                    (_, waitingTime) =>
                    {
                        

                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetRandomRetryPolicy()
        {
            var delay = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5);
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(delay);
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircutBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(10, TimeSpan.FromSeconds(15));
        }



    }
}
