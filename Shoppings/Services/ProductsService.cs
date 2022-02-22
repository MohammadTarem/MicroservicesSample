using System;
using System.Threading.Tasks;
using System.Net.Http;
using Shoppings.Models;
using Newtonsoft.Json;

namespace Shoppings.Services
{
    public class ProductsService 
    {
        private HttpClient http;

        public ProductsService(HttpClient httpClient)
        {
            http = httpClient;
            http.BaseAddress = new Uri(Environment.GetEnvironmentVariable("PRODUCTS"));

        }

        public async Task<Product> GetProductAsync(string id)
        {
            try
            {
                var str = await http.GetStringAsync($"api/v1/product/{id}");
                return JsonConvert.DeserializeObject<Product>(str);

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
