using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Server
{
    public class LinksLifetimeService : IHostedService
    {
        private readonly ILogger<LinksLifetimeService> _logger;
        private readonly IHostApplicationLifetime _lifetime;

        public LinksLifetimeService(ILogger<LinksLifetimeService> logger, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _lifetime = lifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _lifetime.ApplicationStopping.Register(OnApplicationStopping);
            _lifetime.ApplicationStopped.Register(OnApplicationStopped);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnApplicationStopping()
        {
            _logger.LogInformation("Application is stopping, disposing links...");
            try
            {
                Data.DisposeLinks();
                _logger.LogInformation("Links disposed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error occurred while disposing links during application stop");
            }
        }

        private void OnApplicationStopped()
        {
            _logger.LogInformation("Application stopped");
        }
    }
}