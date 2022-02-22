using System;

namespace Shoppings.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }

    }
}
