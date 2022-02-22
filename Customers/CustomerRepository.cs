using Repositoo;
using Customers.Models;
namespace Customers
{
    public class CustomerRepository : BaseRepository<int, Customer>
    {
        public CustomerRepository(InMemoryOperations<int, Customer> operations ) : base(operations)
        {

        }
    }
}
