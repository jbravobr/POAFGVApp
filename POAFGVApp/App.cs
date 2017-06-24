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
            NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes()
        {
            // Registrando Views para navegação
            try
            {
                Container.RegisterTypeForNavigation<LoginPage>();
				Container.RegisterTypeForNavigation<DashboardPage>();
				Container.RegisterTypeForNavigation<BlankPage>();
                Container.RegisterTypeForNavigation<NavigationPage>();

                // Registrando IoC
                Container.RegisterType(typeof(IBaseRepository<>),
                                       typeof(BaseRepository<>),
                                       new ContainerControlledLifetimeManager());

                Container.RegisterType(typeof(IBaseApplicationService<>),
                                      typeof(BaseApplicationService<>),
                                      new ContainerControlledLifetimeManager());
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}