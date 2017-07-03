using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        IPageDialogService _pageServiceDialog { get; }
        INavigationService _navigationService { get; }
        List<Message> _messages { get; set; }

        public NewOrderPageViewModel(IPageDialogService pageServiceDialog, 
                                     INavigationService navigationService)
        {
            _pageServiceDialog = pageServiceDialog;
            _navigationService = navigationService;

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
        }

        Action SendMessage
        {
            get
            {
                return new Action(async () =>
               {
                   if (string.IsNullOrEmpty(Text))
                   {
                       await _pageServiceDialog.DisplayAlertAsync("Aviso", "Você precisa digitar algo para enviar.", "OK");
                       return;
                   }

                   //TODO: CHAMAR SERVIÇO DO BOT.
               });
            }
        }

        async Task NavigateTo(string page)
        {
            await _navigationService.NavigateAsync(page);
        }

        async Task ShowPaymentsOptions()
        {
            await _pageServiceDialog.DisplayActionSheetAsync("Como você vai pagar?", "Cancelar", null, new string[] { "Dinheiro, Débito, Cartão de Crédito" });
        }
    }
}
