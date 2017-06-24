using System;
using Prism.Navigation;
using Prism.Commands;
using Prism.Services;

namespace POAFGVApp.ViewModels
{
    public class LoginPageViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DelegateCommand DoLoginCmd { get; set; }

        INavigationService _navigationService { get; }
        IPageDialogService _pageDialogService { get; }
        IBaseApplicationService<User> _userService { get; }

        public LoginPageViewModel(INavigationService navigationService,
                                  IPageDialogService pageDialogService,
                                  IBaseApplicationService<User> userService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _userService = userService;

            DoLoginCmd = new DelegateCommand(DoLogin);
        }

        Action DoLogin => new Action(async () =>
                                        {
                                            if (string.IsNullOrEmpty(Login) ||
                                               string.IsNullOrEmpty(Password))
                                            {
                                                Login = null;
                                                Password = null;
                                                await _pageDialogService.DisplayAlertAsync("Erro", "Usurio/Senha inválidos", "OK");
                                            }

                                            ////TODO: consultar na fonte os dados

                                            //if (1 == 1)
                                            //await _pageDialogService.DisplayAlertAsync("Navegação", "Acessando página principal", "OK");

                                            await _navigationService.NavigateAsync(new Uri("myapp:///NavigationPage/DashboardPage", UriKind.Absolute));
                                        });
    }
}