using System;
using System.Linq;
using Prism.Navigation;
using Prism.Commands;
using Prism.Services;
using Acr.Settings;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace POAFGVApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DelegateCommand DoLoginCmd { get; set; }

        INavigationService _navigationService { get; }
        IPageDialogService _pageDialogService { get; }
        IUserApplicationService _userService { get; }
        ISettings _settings { get; }

        public LoginPageViewModel(INavigationService navigationService,
                                  IPageDialogService pageDialogService,
                                  IUserApplicationService userService,
                                  ISettings settings)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _userService = userService;
            _settings = settings;

            DoLoginCmd = new DelegateCommand(DoLogin);
        }

        Action DoLogin => new Action(async () =>
                                        {
                                            if (string.IsNullOrEmpty(Login) ||
                                               string.IsNullOrEmpty(Password))
                                            {
                                                Login = null;
                                                Password = null;
                                                await _pageDialogService.DisplayAlertAsync("Erro", "Usuário/Senha inválidos", "OK");
                                                return;
                                            }

                                            if (await GetUser())
                                                await _navigationService.NavigateAsync(new Uri("myapp:///NavigationPage/DashboardPage", UriKind.Absolute));
                                            else
                                            {
                                                await _pageDialogService.DisplayAlertAsync("Erro", "Usuário/Senha inválidos", "OK");
                                                return;

                                            }
                                        });

        async Task<bool> GetUser()
        {
            if (_settings.Get<User>("USER_LOGGED") != null)
                return await Task.FromResult(true);

            Expression<Func<User, bool>> filter = (f) => f.Login.Equals(Login) && f.Password.Equals(Password);
            var user = await _userService.GetAllWithChildrenByPredicateAsync(filter);
            if (user != null)
            {
                _settings.Set<User>("USER_LOGGED", user.FirstOrDefault());
                await _userService.InsertAsync(user.FirstOrDefault());

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}