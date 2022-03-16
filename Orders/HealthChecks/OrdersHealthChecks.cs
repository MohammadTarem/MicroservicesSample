using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Orders
{
    public class OrdersHealthChecks : IHealthCheck
    {
        public OrdersHealthChecks()
        {

        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Orders is working just fine."));
        }
    }
}
