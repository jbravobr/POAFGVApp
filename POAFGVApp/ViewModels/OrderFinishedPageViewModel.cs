using System;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;

namespace POAFGVApp.ViewModels
{
    public class OrderFinishedPageViewModel : BaseViewModel
    {
        /*
         *  <FormattedString>
                <Span
                    Text="Pedido n˚1. Enviado às "
                    FontSize="15" />
                <Span
                    Text="20:37"
                    FontSize="15"
                    FontAttributes="Bold" />
            </FormattedString>
        */
        public Xamarin.Forms.FormattedString OrderDatetimeFormattedString { get; set; }

        /* 1 PIZZA CALABRESA, 1 PIZZA PORTUGUESA */
        public string Products { get; set; }

        /* R$30,00 */
        public string Total { get; set; }
        public DelegateCommand SeeOrderDetailsCmd { get; set; }
        public DelegateCommand BackToHomeCmd { get; set; }
        Order Order { get; set; }

        IUserDialogs _userDialogs { get; }
        INavigationService _navigationSerivce { get; set; }

        public OrderFinishedPageViewModel(IUserDialogs userDialogs,
                                          INavigationService navigationService)
        {
            _userDialogs = userDialogs;
            _navigationSerivce = navigationService;

            SeeOrderDetailsCmd = new DelegateCommand(SeeOrderDetails);
            BackToHomeCmd = new DelegateCommand(BackToHome);
        }

        Action SeeOrderDetails
        {
            get
            {
                return new Action(() =>
                {
                    _userDialogs.Alert("Você clicou em visualizar o pedido!", "Aviso", "OK");
                });
            }
        }

        Action BackToHome
        {
            get
            {
                return new Action(async () =>
                {
                    await _navigationSerivce.NavigateAsync(new Uri("app:///NavigationPage/DashboardPage", UriKind.Absolute));
                });
            }
        }

        void SetViewFields()
        {
            OrderDatetimeFormattedString = new Xamarin.Forms.FormattedString();
            OrderDatetimeFormattedString.Spans.Add(new Xamarin.Forms.Span() { Text = $"Pedido n˚{Order.Id}. Enviado às ", FontSize = 15 });
            OrderDatetimeFormattedString.Spans.Add(new Xamarin.Forms.Span()
            {
                Text = $"{Order.OrderDateTime.Hour}:{Order.OrderDateTime.Minute}",
                FontSize = 15,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold
            });
            Products = Order.FormattedProducts;
            decimal SubTotal = 0;
            Order.OrdersDetail.ForEach((orderItem) =>
                   {
                       orderItem.Products.ForEach((product) =>
                       {
                           SubTotal += product.Price;
                       });
                   });
            Total = $"R${SubTotal.ToString()}";
        }

        public override void OnNavigatingTo(Prism.Navigation.NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Order"))
            {
                Order = parameters["Order"] as Order;
                SetViewFields();
            }
        }
    }
}

