using System;
using System.Threading.Tasks;
using System.Net.Http;
using Shoppings.Models;
using Newtonsoft.Json;



namespace Shoppings.Services
{
    public class OrdersService
    {
        private HttpClient http;
        public OrdersService(HttpClient httpClient)
        {
            http = httpClient;
            http.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ORDERS"));
        }

        public async Task<Order> GetOrdersAsync(string id)
        {
            try
            {
                var str = await http.GetStringAsync($"api/v1/order/{id}");
                return JsonConvert.DeserializeObject<Order>(str);

            }
            catch (HttpRequestException e)
            {

                if (e.Message.Contains("404"))
                {
                    return null;
                }
                else
                {
                    throw e;
                }

            }
        }
    }
}
