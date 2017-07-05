using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace POAFGVApp.ViewModels
{
    public class ConfirmOrderPageViewModel : BaseViewModel
    {
        INavigationService _navigationSerivce { get; }
        IBaseApplicationService<Order> _orderService { get; }
        IUserApplicationService _userService { get; }
        IPageDialogService _pageDialogService { get; }

        public DelegateCommand FinishOrderCmd { get; set; }
        public Order PreOrder { get; set; }

        #region View Fields

        public string OrderID { get; set; }
        public List<ItemsFromOrder> Items { get; set; }
        public string Total { get; set; }
        public DateTimeOffset OrderDatetime { get; set; }
        public string Payment { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Burgh { get; set; }

        #endregion

        public ConfirmOrderPageViewModel(INavigationService navigationService,
                                         IBaseApplicationService<Order> orderService,
                                         IPageDialogService pageDialogService,
                                         IUserApplicationService userService)
        {
            _navigationSerivce = navigationService;
            _orderService = orderService;
            _pageDialogService = pageDialogService;
            _userService = userService;

            FinishOrderCmd = new DelegateCommand(FinishOrder);
        }

        Action FinishOrder
        {
            get
            {
                return new Action(async () =>
               {
                   var confirm = await _pageDialogService.DisplayAlertAsync("Aviso", "Deseja confirmar este pedido?", "Sim", "Não");
                   if (confirm)
                   {
                       var orderDone = await _orderService.InsertAsync(PreOrder);
                       if (orderDone > -1)
                           await NavigateTo("OrderFinishedPage", PreOrder);
                       else
                           await _pageDialogService.DisplayAlertAsync("Erro",
                                                                      "Ops... tivemos um problema para receber o seu pedido, por favor tente novamente",
                                                                      "OK");
                   }
                   else
                       return;
               });
            }
        }

        async Task SetupViewFields()
        {
            try
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
                OrderDatetime = PreOrder.OrderDateTime;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Order"))
            {
                PreOrder = parameters["Order"] as Order;
                await SetupViewFields();
            }
        }

        private async Task NavigateTo(string page, Order order)
        {
            var navParameters = new NavigationParameters()
            {
                {"Order", order}
            };

            await _navigationSerivce.NavigateAsync(page, navParameters);
        }
    }
}