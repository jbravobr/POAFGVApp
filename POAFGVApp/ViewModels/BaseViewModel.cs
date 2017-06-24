using System;
using Prism.Navigation;

namespace POAFGVApp
{
    public class BaseViewModel : INavigationAware, IDestructible
    {
        public BaseViewModel()
        {
        }

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