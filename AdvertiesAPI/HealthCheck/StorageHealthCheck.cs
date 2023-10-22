using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using System.Threading;
using AdvertiesAPI.Services;

namespace AdvertiesAPI.HealthCheck
{
    public class StorageHealthCheck : IHealthCheck
    {
        private readonly IAdvertiesStorageService _storageService;

        public StorageHealthCheck(IAdvertiesStorageService storageService)
        {
            _storageService = storageService;
        }


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var isStorageOk = await _storageService.CheckHealthAsync();
            return new HealthCheckResult(isStorageOk ? HealthStatus.Healthy : HealthStatus.Unhealthy);
        }
    }
}
