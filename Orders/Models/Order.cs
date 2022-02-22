using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Models
{
    public class Order
    {

        public string Id { get; protected set; }
        public DateTime OrderDate { get; protected set; }
        public Buyer Customer { get; protected set; }
        protected List<Item> products;
        

        public Order(string id, DateTime orderDate, Buyer customer )
        {
            Id = id;
            OrderDate = orderDate;
            Customer = customer;
            products = new List<Item>();

        }

        public Order(Buyer customer)
            :this(Guid.NewGuid().ToString(), DateTime.Now, customer)
        { }

        public double TotalPrice => Products.Sum(p => p.Price);

        public void AddProduct(Item p)
        {
            products.Add(p);
        }

        public void RemoveProduct(string productNumber)
        {
            products = products.Where(p => p.ProductNumber != productNumber).ToList();
        }

        public IList<Item> Products
        {
            get
            {
                return products.Select(
                    (p) =>
                    {
                        return new Item {Name = p.Name, ProductNumber = p.ProductNumber, Price = p.Price };
                    })
                    .ToList();
            }
        }




    }
}
