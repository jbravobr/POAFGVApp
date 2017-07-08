using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Humanizer;
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
        string[] pagamentos = new[] { "Dinheiro", "Débito", "Cartão de Crédito", "Cartao de Credito", "Cartao de Debito" };
        string[] existProducts = new[] { "Calabresa", "Portuguesa", "Muçarela" };

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
            if (!_botConnector.IsBotConnectorConfigured())
                await _botConnector.Setup();

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
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => _userDialogs.ShowLoading("Finalizando Pedido", MaskType.Black));
                await Task.Delay(3000);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(_userDialogs.HideLoading);
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

                                                   var selectedFlavours = existProducts.ToList()
                                                                         .Where(x => Text.ToLower().Contains(x.ToLower()));

                                                   var products = new List<Product>();
                                                   if (selectedFlavours != null && selectedFlavours.Any())
                                                   {
                                                       selectedFlavours.ForEach(async delegate (string flavour)
                                                       {
                                                           if (PreOrder.OrdersDetail == null)
                                                               PreOrder.OrdersDetail = new List<OrderDetail>();

                                                           var titleFlavour = flavour.Humanize(LetterCasing.Title);
                                                           Func<Product, bool> filter = (f) => f.Description.Equals(titleFlavour);
                                                           var product = await _productService.GetWithChildrenByPredicateAsync(filter);

                                                           if (product != null)
                                                               products.Add((product));
                                                           else
                                                               products.Add(new Product() { Description = titleFlavour, Price = 30.00M });
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
