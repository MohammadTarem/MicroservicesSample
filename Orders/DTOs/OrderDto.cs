using System;
using System.ComponentModel.DataAnnotations;

namespace Orders.Controllers
{
    public class OrderDto
    {
        

        [Required]
        public DateTime OrderDate { get; protected set; }

        public class CustomerDto
        {
            [Required]
            public int Id { get; set; }

            [Required]
            [StringLength(100)]
            public string FullName { get; set; }

            [Required]
            [StringLength(200)]
            public string ShippingAddress { get; set; }
        }

        public class ProductDto
        {
            [Required]
            [StringLength(30)]
            public string ProductNumber { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [Required]
            public double Price { get; set; }
        }

        [Required]
        public CustomerDto Customer { get; set; }

        [Required]
        [MinLength(1)]
        public ProductDto[] products { get; set; }

    }
}
