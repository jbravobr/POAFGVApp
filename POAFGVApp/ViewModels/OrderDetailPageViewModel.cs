using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Services;

namespace POAFGVApp.ViewModels
{
    public class OrderDetailPageViewModel : BaseViewModel
    {
        public Order PreOrder { get; set; }

        IPageDialogService _pageDialogService { get; }
        IUserApplicationService _userService { get; }
        IBaseApplicationService<Order> _orderService { get; }

        public OrderDetailPageViewModel(IUserApplicationService userService,
                                        IBaseApplicationService<Order> orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        #region View Fields

        public string OrderID { get; set; }
        public List<ItemsFromOrder> Items { get; set; }
        public string Total { get; set; }
        public string OrderDatetime { get; set; }
        public string Payment { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Burgh { get; set; }

        #endregion

        async Task SetupViewFields()
        {
            var lastOder = (await _orderService.GetAllAsync());
            OrderID = lastOder != null && lastOder.Any() ? $"Pedido #{lastOder.Last().Id + 1}" : "Pedido #1";

            Items = new List<ItemsFromOrder>();
            decimal SubTotal = 0;
            PreOrder.OrdersDetail.ForEach((orderItem) =>
               {
                   orderItem.Products.ForEach((product) =>
                   {
                       Items.Add(new ItemsFromOrder() { Item = product.Description, Price = $"R$ {product.Price.ToString()}" });
                       SubTotal += product.Price;
                   });
               });
            Total = $"R${SubTotal.ToString()}";
            OrderDatetime = $"{PreOrder.OrderDateTime.ToString("dd/MM/yyyy")} {PreOrder.OrderDateTime.Hour}:{PreOrder.OrderDateTime.Minute}:{PreOrder.OrderDateTime.Second}";
            Payment = PreOrder.PaymentType;

            var userAddress = await _userService.GetAllAsync();
            if (userAddress != null && userAddress.Any())
            {
                Address = userAddress.FirstOrDefault().Address.Street;
                Number = userAddress.FirstOrDefault().Address.Number.ToString();
                Burgh = userAddress.FirstOrDefault().Address.Burgh;
            }
            else
            {
                Address = "Rua Maia de Lacerda";
                Number = "186 - Apartamento 202";
                Burgh = "Estácio - Rio de Janeiro";
            }
        }

        public async override void OnNavigatingTo(Prism.Navigation.NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Order"))
            {
                PreOrder = parameters["Order"] as Order;
                await SetupViewFields();
            }
        }
    }
}