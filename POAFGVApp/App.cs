using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using System;

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
            throw new NotImplementedException();
        }
    }
}