using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using System;
using POAFGVApp.Views;
using Microsoft.Practices.Unity;
using System.Net.Http;

namespace POAFGVApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class App : PrismApplication
    {
        public static SQLite.SQLiteConnection AppSQLiteConn { get; set; }
        public static HttpClient AppHttpClient { get; set; }
        public static bool IsBotConnectorConfigured { get; set; } = false;

        protected override void OnInitialized()
        {
            try
            {
                NavigationService.NavigateAsync("NewOrderPage");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void RegisterTypes()
        {
            // Registrando Views para navegação
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<DashboardPage>();
            Container.RegisterTypeForNavigation<BlankPage>();
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<NewOrderPage>();
            Container.RegisterTypeForNavigation<ListOrdersPage>();
            Container.RegisterTypeForNavigation<OrderDetailPage>();
            Container.RegisterTypeForNavigation<ConfirmOrderPage>();
            Container.RegisterTypeForNavigation<OrderFinishedPage>();

            // Registrando IoC
            Container.RegisterType(typeof(IBaseRepository<>),
                                           typeof(BaseRepository<>),
                                           new ContainerControlledLifetimeManager());

            Container.RegisterType(typeof(IBaseApplicationService<>),
                                  typeof(BaseApplicationService<>),
                                  new ContainerControlledLifetimeManager());

            Container.RegisterType(typeof(IUserApplicationService),
                                  typeof(UserApplicationServices),
                                  new ContainerControlledLifetimeManager());

            Container.RegisterInstance(Acr.Settings.Settings.Current);

            Container.RegisterType(typeof(IBotConnector), typeof(BotConnector));
        }
    }
}