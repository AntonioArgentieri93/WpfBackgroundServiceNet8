using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WpfBackgroundServiceNet8_2.Services
{
    /// <summary>
    /// Initialize background operations
    /// </summary>
    public class BackgroundWorkerInitializer : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public BackgroundWorkerInitializer(ILogger<BackgroundWorkerInitializer> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(StartAsync)} of {nameof(BackgroundWorkerInitializer)} called - {DateTime.Now}");

            await InitOperationAsync().ConfigureAwait(false);

            _logger.LogInformation($"{nameof(StartAsync)} of {nameof(BackgroundWorkerInitializer)} completed {DateTime.Now}");
        }

        public async Task StopAsync(CancellationToken cancellationToken) 
        {
            _logger.LogInformation($"{nameof(StopAsync)} of {nameof(BackgroundWorkerInitializer)} STARTED - {DateTime.Now}");

            await Task.Delay(2000).ConfigureAwait(false);

            _logger.LogInformation($"{nameof(StopAsync)} of {nameof(BackgroundWorkerInitializer)} ENDED - {DateTime.Now}");
        } 

        private async Task InitOperationAsync()
        {
            _logger.LogInformation($"{nameof(InitOperationAsync)} STARTED - {DateTime.Now}");

            await Task.Delay(1000);

            _logger.LogInformation($"{nameof(InitOperationAsync)} ENDED - {DateTime.Now}");
        }
    }
}
