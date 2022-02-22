using System;

namespace Orders.Models
{
    public class Buyer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShipingAddress { get; set; }
    }
}
