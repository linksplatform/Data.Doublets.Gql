using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Server
{
    public class GracefulShutdownService : IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<GracefulShutdownService> _logger;

        public GracefulShutdownService(IHostApplicationLifetime applicationLifetime, ILogger<GracefulShutdownService> logger)
        {
            _applicationLifetime = applicationLifetime;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnApplicationStopping()
        {
            _logger.LogInformation("Application is stopping. Initiating graceful shutdown for WebSocket connections...");
            
            // Give some time for active connections to close gracefully
            Thread.Sleep(TimeSpan.FromSeconds(2));
            
            _logger.LogInformation("Graceful shutdown completed.");
        }
    }
}