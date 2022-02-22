using System;
using Repositoo;
using Products.Models;
namespace Products
{
    public class ProductRepository : BaseRepository<string, Product>
    {
        public ProductRepository(InMemoryOperations<string, Product> operations) : base(operations)
        {
        }
    }
}
