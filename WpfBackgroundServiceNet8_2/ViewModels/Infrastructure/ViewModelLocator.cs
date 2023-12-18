using Microsoft.Extensions.DependencyInjection;

namespace WpfBackgroundServiceNet8_2.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
    }
}
