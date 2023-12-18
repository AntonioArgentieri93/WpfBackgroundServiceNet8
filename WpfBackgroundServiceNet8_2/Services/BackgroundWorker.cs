using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WpfBackgroundServiceNet8_2.Services
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly ILogger<BackgroundWorker> _logger;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _operationInProgress;

        public BackgroundWorker(ILogger<BackgroundWorker> logger)
        {
            _logger = logger;    
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(StartAsync)} of {nameof(BackgroundWorker)} called - {DateTime.Now}");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //Prevent to ExecuteAsync to be executed during stop operations
                var linkedSourceToken = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken, _cts.Token).Token;

                while(stoppingToken.IsCancellationRequested == false)
                {
                    //Prevent new BackgroundLongOperation to be executed before previous BackgroundLongOperation was ended
                    if (_operationInProgress == false)
                    {
                        _operationInProgress = true;

                        await BackgroundLongOperation();

                        _operationInProgress = false;
                    }

                    await Task.Delay(2000, linkedSourceToken);
                }
            }
            catch (TaskCanceledException)
            {
                _logger.LogError($"TaskCanceledException - {DateTime.Now}");
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();

            _logger.LogInformation($"{nameof(StopAsync)} of {nameof(BackgroundWorker)} STARTED - {DateTime.Now}");

            await Task.Delay(2000);

            _logger.LogInformation($"{nameof(StopAsync)} of {nameof(BackgroundWorker)} ENDED - {DateTime.Now}");
        }

        private async Task BackgroundLongOperation()
        {
            _logger.LogInformation($"Execution at {DateTime.Now}");

            await Task.Delay(5000);
        }
    }
}
