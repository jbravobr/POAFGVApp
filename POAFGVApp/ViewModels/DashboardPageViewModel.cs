using System;
using Prism.Commands;
using Prism.Services;

namespace POAFGVApp.ViewModels
{
    public class DashboardPageViewModel : BaseViewModel
    {
        public DelegateCommand<string> MenuSelectCmd { get; set; }

        IPageDialogService _pageDialogService { get; }

        public DashboardPageViewModel(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;

            MenuSelectCmd = new DelegateCommand<string>(MenuSelect);
        }

        Action<string> MenuSelect
        {
            get
            {
                return new Action<string>(async (menu) =>
                {
                    switch (menu)
                    {
                        case "1":
                            //await _pageDialogService.DisplayAlertAsync("Clique no menu", "Menu Novo Pedido", "OK");
                            Xamarin.Forms.MessagingCenter.Send(this, "Clique");
                            break;

                        case "2":
                            await _pageDialogService.DisplayAlertAsync("Clique no menu", "Menu Lista de Pedidos", "OK");
                            Xamarin.Forms.MessagingCenter.Send(this, "Clique2");
                            break;

                        default:
                            await _pageDialogService.DisplayAlertAsync("Clique no menu", "Erro, menu não reconhecido", "OK");
                            break;
                    }
                });
            }
        }
    }
}