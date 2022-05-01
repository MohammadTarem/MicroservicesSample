using System;
using System.Threading.Tasks;
using System.Net.Http;
using Shoppings.Models;
using Newtonsoft.Json;

namespace Shoppings.Services
{
    public sealed class CustomersService
    {
        private HttpClient http;

        public CustomersService(HttpClient httpClient)
        {
            http = httpClient;
            http.BaseAddress = new Uri(Environment.GetEnvironmentVariable("CUSTOMERS"));
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            try
            {
                var str = await http.GetStringAsync($"api/v1/customer/{id}");
                return JsonConvert.DeserializeObject <Customer>(str);

            }
            catch(HttpRequestException e)
            {
                
                if(e.Message.Contains("404"))
                {
                    return null;
                }
                else
                {
                    throw e;
                }

            }

        }

        public async Task GetErrorAsync() => await http.GetStringAsync($"api/v1/customer/error");
        
    }
}
