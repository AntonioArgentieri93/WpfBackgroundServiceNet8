using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Threading;
using WpfBackgroundServiceNet8_2.Services;
using WpfBackgroundServiceNet8_2.ViewModels;

namespace WpfBackgroundServiceNet8_2
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            ServiceProvider = _host.Services;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Close();

            var frame = new DispatcherFrame(false);

            Task.Run(async () =>
            {
                try
                {
                    await _host.StopAsync();
                }
                finally
                {
                    frame.Continue = false;
                }
            });

            Dispatcher.PushFrame(frame);

            _host.Dispose();
            _host = null;

            base.OnExit(e);
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            //Services
            services.AddHostedService<BackgroundWorkerInitializer>();
            services.AddHostedService<BackgroundWorker>();

            //Views
            services.AddSingleton<MainWindow>();

            //ViewModels
            services.AddSingleton<MainViewModel>();
        }
    }
}
