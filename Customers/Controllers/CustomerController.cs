using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Customers.Controllers;
using Customers.Models;
using Microsoft.Extensions.Logging;

namespace Customers.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository customers;
        
        public CustomerController(CustomerRepository repo)
        {
            customers = repo;
            
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = customers.FirstOrDefault( c => c.Id == id);
            if(customer == null)
            {
                return NotFound();
            }

            return new JsonResult(customer);

        }

        [HttpPost]
        public IActionResult Post([FromBody]CustomerDto customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newCustomer = new Customer();
            newCustomer.Id = customers.Count() + 1;
            newCustomer.Name = customer.Name;
            newCustomer.Lastname = customer.Lastname;
            newCustomer.Address = customer.Address;

            customers.Add(newCustomer);

            return  new JsonResult(new { newCustomer.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerDto customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var found = customers.FirstOrDefault(c => c.Id == id);
            if(found == null)
            {
                return NotFound();
            }

            found.Name = customer.Name;
            found.Lastname = customer.Lastname;
            found.Address = customer.Address;

            customers.Update(found);

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var found = customers.FirstOrDefault(c => c.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            customers.Delete(found);
            return Ok();

        }

        [HttpGet("error")]
        public IActionResult ReturnError()
        {
            
            System.Diagnostics.Debug.WriteLine($"Error 500 has been generated.");
            return StatusCode(500);

        }

    }
}
