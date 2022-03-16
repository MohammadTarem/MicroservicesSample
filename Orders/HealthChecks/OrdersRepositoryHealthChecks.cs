using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;

namespace Orders
{
    public class OrdersRepositoryHealthChecks : IHealthCheck
    {
        OrdersRepository orders;
        public OrdersRepositoryHealthChecks(OrdersRepository ordersRepo)
        {
            orders = ordersRepo;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if(orders.Count() > 1)
            {
                return Task.FromResult(HealthCheckResult.Degraded("Memory is going to be full."));

            }
            else
            {
                return Task.FromResult(HealthCheckResult.Healthy("Orders repository is healthy"));
            }
            
        }
    }
}
