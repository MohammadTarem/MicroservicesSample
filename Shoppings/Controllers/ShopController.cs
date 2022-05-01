using Microsoft.AspNetCore.Mvc;
using Shoppings.Services;
using System.Threading.Tasks;

namespace Shoppings.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private CustomersService customers;
        private ProductsService products;
        private OrdersService orders;
        public ShopController(CustomersService customersService, ProductsService productsService, OrdersService ordersService)
        {
            customers = customersService;
            products = productsService;
            orders = ordersService;

        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await customers.GetCustomerAsync(id);
            if(customer == null)
            {
                return NotFound();
            }

            return new JsonResult(customer);
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await products.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return new JsonResult(product);
        }

        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            var order = await orders.GetOrdersAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return new JsonResult(order);
        }

        [HttpGet("error")]
        public async Task<IActionResult> GetError()
        {
            await customers.GetErrorAsync();

            return Ok();
        }


    }
}
