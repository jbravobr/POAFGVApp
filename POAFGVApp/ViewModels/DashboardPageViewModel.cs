using System;
using System.Threading.Tasks;
using POAFGVApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace POAFGVApp.ViewModels
{
    public class DashboardPageViewModel : BaseViewModel
    {
        public DelegateCommand<string> MenuSelectCmd { get; set; }

        IPageDialogService _pageDialogService { get; }
        INavigationService _navigationSevice { get; }

        public DashboardPageViewModel(IPageDialogService pageDialogService, INavigationService navigationService)
        {
            _pageDialogService = pageDialogService;
            _navigationSevice = navigationService;

            MenuSelectCmd = new DelegateCommand<string>(MenuSelect);
            Xamarin.Forms.MessagingCenter.Subscribe<DashboardPage, string>(this, "NewOrder", async (page, menu) => await NavigateTo(menu));
        }

        private async Task NavigateTo(string menu)
        {
            await _navigationSevice.NavigateAsync(menu);
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
                            Xamarin.Forms.MessagingCenter.Send(this, "ClickMenuNewOrder");
                            break;

                        case "2":
                            Xamarin.Forms.MessagingCenter.Send(this, "ClickMenuOrderList");
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