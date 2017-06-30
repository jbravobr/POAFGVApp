using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public ListOrdersPageViewModel(INavigationService navigationService,
                                       IBaseApplicationService<Order> orderService)
        {
            _navigationService = navigationService;
            _orderService = orderService;

            SelectItemCmd = new DelegateCommand<Order>(SelectItem);
            Orders = new ObservableCollection<Order>(OrderHelper.Orders);
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

            await _navigationService.NavigateAsync("OrderDetailPage", navParameters);
        }
    }
}