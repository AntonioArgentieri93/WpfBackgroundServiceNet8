using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfBackgroundServiceNet8_2.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _title = "WpfBackgroundServiceNet8_2";

        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value, nameof(Title));
            }
        }
    }
}
