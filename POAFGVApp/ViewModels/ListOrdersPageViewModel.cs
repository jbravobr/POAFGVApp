using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;

namespace POAFGVApp.ViewModels
{
    public class ListOrdersPageViewModel : BaseViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }
        public DelegateCommand<Order> SelectItemCmd { get; set; }

        INavigationService _navigationService { get; }
        IBaseApplicationService<Order> _orderService { get; }
        IUserDialogs _userDialogs { get; }

        public ListOrdersPageViewModel(INavigationService navigationService,
                                       IBaseApplicationService<Order> orderService,
                                       IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _orderService = orderService;
            _userDialogs = userDialogs;

            SelectItemCmd = new DelegateCommand<Order>(SelectItem);
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            await LoadLocalOrders();
        }

        async Task LoadLocalOrders()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => _userDialogs.ShowLoading("Carregando seus pedidos"));
            var orders = await _orderService.GetAllAsync();
            if (orders != null && orders.Any())
                Orders = new ObservableCollection<Order>(orders);
            else
                Orders = new ObservableCollection<Order>(OrderHelper.Orders);
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => _userDialogs.HideLoading());
        }

        Action<Order> SelectItem
        {
            get
            {
                return new Action<Order>(async (Order o) =>
                {
                    await NavigateTo(o);
                });
            }
        }

        async Task NavigateTo(Order order)
        {
            var navParameters = new NavigationParameters()
            {
                {"Order", order}
            };

            App.IsFromListOrders = true;
            await _navigationService.NavigateAsync("OrderDetailPage", navParameters);
        }
    }
}