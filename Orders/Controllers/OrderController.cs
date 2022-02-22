using Orders.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace Orders.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private OrdersRepository orders;
        public OrderController( OrdersRepository repo)
        {
            orders = repo;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if(order == null)
            {
                return NotFound();
            }

            return new JsonResult(order);

        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderDto order)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest();

            }

            var customer = new Buyer();
            customer.Id = order.Customer.Id;
            customer.FullName = order.Customer.FullName;
            customer.ShipingAddress =  order.Customer.ShippingAddress;

            var newOrder = new Order(customer);
            order.products
              .Select(
                (p) =>
                {
                    return new Item { Name = p.Name, ProductNumber = p.ProductNumber, Price = p.Price };
                    
                })
              .ToList()
              .ForEach(p => newOrder.AddProduct(p));


            orders.Add(newOrder);
            return new JsonResult(new { newOrder.Id });
        }






    }
}
