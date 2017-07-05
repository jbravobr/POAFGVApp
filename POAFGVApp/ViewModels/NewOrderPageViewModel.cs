using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Acr.UserDialogs;

namespace POAFGVApp.ViewModels
{
    public class NewOrderPageViewModel : BaseViewModel
    {
        public ObservableCollection<BotMessage> Messages { get; set; }
        public DelegateCommand SendMessageCmd { get; set; }
        public string Text { get; set; }
        public bool IsRunning { get; set; } = false;
        public bool IsVisible { get; set; } = false;

        IPageDialogService _pageServiceDialog { get; }
        IBaseApplicationService<Product> _productService { get; }
        INavigationService _navigationService { get; }
        IBotConnector _botConnector { get; }
        IUserDialogs _userDialogs { get; }

        Order PreOrder { get; set; }
        List<BotMessage> _messages { get; set; } = new List<BotMessage>();
        string[] sabores = new[] { "calabresa", "portuguesa", "peperoni", "calabreza", "peperonni", "muçarela", "mussarela" };
        string[] pagamentos = new[] { "Dinheiro", "Débito", "Cartão de Crédito", "Cartao de Credito", "Cartao de Debito" };

        public NewOrderPageViewModel(IPageDialogService pageServiceDialog,
                                     INavigationService navigationService,
                                     IBotConnector botConnector,
                                     IBaseApplicationService<Product> productService,
                                     IUserDialogs userDialogs)
        {
            _productService = productService;
            _pageServiceDialog = pageServiceDialog;
            _navigationService = navigationService;
            _botConnector = botConnector;
            _userDialogs = userDialogs;

            AddNewMessageToChat("Olá, bem vindo a Pizzaria Pastaquepariu!", true);
            PreOrder = new Order
            {
                OrderDateTime = DateTime.Now
            };

            SendMessageCmd = new DelegateCommand(SendMessage);
        }

        void AddNewMessageToChat(string text, bool isIncoming)
        {
            var msg = new BotMessage()
            {
                Text = text,
                IsIncoming = isIncoming,
                MessageDateTime = DateTime.Now
            };
            _messages.Add(msg);

            Messages = new ObservableCollection<BotMessage>(_messages);
        }

        async Task<BotMessage> CallBotService()
        {
            if (!App.IsBotConnectorConfigured)
                App.IsBotConnectorConfigured = await _botConnector.Setup();

            if (!App.IsBotConnectorConfigured)
            {
                await _pageServiceDialog.DisplayAlertAsync("Erro", "Não conectado ao Bot.", "OK");
                return null;
            }

            return await _botConnector.SendMessage("Rodrigo", Text);
        }

        void ActivityIndicatorChanged(bool status)
        {
            IsVisible = status;
            IsRunning = status;
        }

        async Task FinalizeSession(BotMessage message)
        {
            if (message.Text == "Ótimo, então vamos confirmar o seu pedido!")
            {
                _userDialogs.ShowLoading("Finalizando Pedido", MaskType.Black);
                await Task.Delay(5000);
                _userDialogs.HideLoading();
                await NavigateTo("ConfirmOrderPage", PreOrder);
            }
        }

        Action SendMessage => new Action(async () =>
                                           {
                                               if (IsRunning)
                                                   return;

                                               AddNewMessageToChat(Text, false);
                                               Xamarin.Forms.MessagingCenter.Send<NewOrderPageViewModel, List<BotMessage>>(this, "ScrollTo", _messages);
                                               ActivityIndicatorChanged(true);

                                               if (string.IsNullOrEmpty(Text))
                                               {
                                                   IsVisible = false;
                                                   IsRunning = false;
                                                   await _pageServiceDialog.DisplayAlertAsync("Aviso", "Você precisa digitar algo para enviar.", "OK");
                                                   return;
                                               }

                                               var botMessage = await CallBotService();
                                               if (botMessage == null)
                                                   return;

                                               ActivityIndicatorChanged(false);

                                               if (botMessage != null)
                                               {
                                                   AddNewMessageToChat(botMessage.Text, true);
                                                   Xamarin.Forms.MessagingCenter.Send<NewOrderPageViewModel, List<BotMessage>>(this, "ScrollTo", _messages);

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
                                               await Task.Delay(5000);
                                               await FinalizeSession(botMessage);
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
