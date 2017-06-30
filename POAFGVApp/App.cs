using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using System;
using POAFGVApp.Views;
using Microsoft.Practices.Unity;

namespace POAFGVApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class App : PrismApplication
    {
        public static SQLite.SQLiteConnection AppSQLiteConn { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                NavigationService.NavigateAsync("OrderDetailPage");
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
        }
    }
}