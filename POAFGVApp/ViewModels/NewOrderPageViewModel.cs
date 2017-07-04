using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace POAFGVApp.ViewModels
{
    public class NewOrderPageViewModel : BaseViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }
        public DelegateCommand SendMessageCmd { get; set; }
        public string Text { get; set; }
        public bool IsRunning { get; set; } = false;
        public bool IsVisible { get; set; } = false;

        IPageDialogService _pageServiceDialog { get; }
        IBaseApplicationService<Product> _productService { get; }
        INavigationService _navigationService { get; }
        IBotConnector _botConnector { get; }
        List<Message> _messages { get; set; }
        string[] sabores = new[] { "calabresa", "portuguesa", "peperoni", "calabreza", "peperonni", "muçarela", "mussarela" };
        string[] pagamentos = new[] { "Dinheiro", "Débito", "Cartão de Crédito", "Cartao de Credito", "Cartao de Debito" };

        public NewOrderPageViewModel(IPageDialogService pageServiceDialog,
                                     INavigationService navigationService,
                                     IBotConnector botConnector,
                                     IBaseApplicationService<Product> productService)
        {
            _productService = productService;
            _pageServiceDialog = pageServiceDialog;
            _navigationService = navigationService;
            _botConnector = botConnector;

            _messages = new List<Message>()
            {
                new Message()
                {
                    Text = "Olá, bem vindo a Pizzaria Pastaquepariu!",
                     IsIncoming = true,
                    MessageDateTime = DateTime.Now
                }
            };

            Messages = new ObservableCollection<Message>(_messages);
            SendMessageCmd = new DelegateCommand(SendMessage);
        }

        Action SendMessage => new Action(async () =>
                                           {
                                               if (IsRunning)
                                                   return;

                                               var msg = new Message()
                                               {
                                                   Text = Text,
                                                   IsIncoming = false,
                                                   MessageDateTime = DateTime.Now
                                               };
                                               _messages.Add(msg);

                                               Messages = new ObservableCollection<Message>(_messages);

                                               IsVisible = true;
                                               IsRunning = true;

                                               if (string.IsNullOrEmpty(Text))
                                               {
                                                   IsVisible = false;
                                                   IsRunning = false;
                                                   await _pageServiceDialog.DisplayAlertAsync("Aviso", "Você precisa digitar algo para enviar.", "OK");
                                                   return;
                                               }

                                               if (!App.IsBotConnectorConfigured)
                                                   App.IsBotConnectorConfigured = await _botConnector.Setup();

                                               if (!App.IsBotConnectorConfigured)
                                               {
                                                   await _pageServiceDialog.DisplayAlertAsync("Erro", "Não conectado ao Bot.", "OK");
                                                   return;
                                               }

                                               var result = await _botConnector.SendMessage("Rodrigo", Text);
                                               IsVisible = false;
                                               IsRunning = false;

                                               var PreOrder = new Order
                                               {
                                                   OrderDateTime = DateTime.Now
                                               };

                                               if (result != null)
                                               {
                                                   msg = new Message()
                                                   {
                                                       Text = result.Text,
                                                       IsIncoming = true,
                                                       MessageDateTime = DateTime.Now
                                                   };
                                                   _messages.Add(msg);

                                                   Messages = new ObservableCollection<Message>(_messages);
                                                   var flavours = sabores.ToList()
                                                                         .Where(x => Text.ToLower().Contains(x));


                                                   var products = new List<Product>();

                                                   if (flavours != null && flavours.Any())
                                                   {
                                                       flavours.ForEach(delegate (string f)
                                                       {
                                                           if (PreOrder.OrdersDetail == null)
                                                               PreOrder.OrdersDetail = new List<OrderDetail>();

                                                           products.Add(new Product() { Description = f.ToUpper(), Price = 30.00M });
                                                       });

                                                       PreOrder.OrdersDetail.Add(new OrderDetail() { Products = products });
                                                   }

                                                   var payments = pagamentos.ToList()
                                                                            .Where(x => Text.ToLower().Contains(x.ToLower()));

                                                   if (payments != null && payments.Any())
                                                   {
                                                       PreOrder.PaymentType = payments.FirstOrDefault();
                                                   }
                                               }
                                               Text = "";

                                               if (result.Text == "Ótimo, então vamos confirmar o seu pedido!")
                                               {
                                                   await Task.Delay(2000);
                                                   await NavigateTo("ConfirmOrderPage", PreOrder);
                                               }
                                           });

        async Task NavigateTo(string page, Order preOrder)
        {
            var naviParameters = new NavigationParameters
            {
                {"Order", preOrder}
            };
            await _navigationService.NavigateAsync(page, naviParameters);
        }
    }
}
