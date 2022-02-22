using System;
using System.Collections.Generic;
using System.Linq;

namespace Shoppings.Models
{
    public class Order
    {

        public string Id { get; set; }
        public DateTime OrderDate { get;  set; }
        public Buyer Customer { get;  set; }
        public List<Item> Products { get; set; }


    }
}
