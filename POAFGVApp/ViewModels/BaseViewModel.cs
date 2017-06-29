using System.ComponentModel;
using Prism.Navigation;

namespace POAFGVApp
{
    public class BaseViewModel : INavigationAware, IDestructible, INotifyPropertyChanged
    {
        public BaseViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Destroy()
        {
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}