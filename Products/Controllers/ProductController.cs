using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Products.Models;


namespace Products.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private ProductRepository products;
        public ProductController(ProductRepository repo)
        {
            products = repo;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var product = products.FirstOrDefault(p => p.ProductNumber == id);
            if (product == null)
            {
                return NotFound();
            }

            return new JsonResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newProduct = new Product();
            newProduct.ProductNumber = Guid.NewGuid().ToString().Substring(0, 8);
            newProduct.Name = product.Name;
            newProduct.Manufacturer = product.Manufacturer;
            newProduct.Price = product.Price;

            products.Add(newProduct);

            return new JsonResult(new { newProduct.ProductNumber });
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var found = products.FirstOrDefault(c => c.ProductNumber == id);
            if (found == null)
            {
                return NotFound();
            }

            found.Name = product.Name;
            found.Manufacturer = product.Manufacturer;
            found.Price = product.Price;

            products.Update(found);

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var found = products.FirstOrDefault(c => c.ProductNumber == id);
            if (found == null)
            {
                return NotFound();
            }

            products.Delete(found);
            return Ok();

        }



    }
}
