using Orders.Models;
using Repositoo;
namespace Orders
{
    public class OrdersRepository : BaseRepository<string, Order>
    {
        public OrdersRepository(InMemoryOperations<string, Order> operations) : base(operations)
        {

        }

       
    }
}
