using System;
using Customers.Models;
using System.ComponentModel.DataAnnotations;

namespace Customers.Controllers
{
    public class CustomerDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Lastname { get; set; }

        [Required()]
        public Address Address { get; set; }
    }
}
