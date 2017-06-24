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
            throw new NotImplementedException();
        }

        protected override void RegisterTypes()
        {
            // Registrando Views para navegação
            Container.RegisterTypeForNavigation<LoginPage>();
			Container.RegisterTypeForNavigation<NavigationPage>();

			// Registrando IoC
			Container.RegisterType(typeof(IBaseRepository<>),
                                   typeof(BaseRepository<>),
                                   new ContainerControlledLifetimeManager());

            Container.RegisterType(typeof(IBaseApplicationService<>),
                                  typeof(BaseApplicationService<>),
                                  new ContainerControlledLifetimeManager());
        }
    }
}