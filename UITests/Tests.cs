using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace POAFGVApp.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void NewTest()
        {
            app.Tap(x => x.Marked("txtCaixaDeTexto"));
            app.Screenshot("Tapped on view with class: EntryEditText");
            app.EnterText(x => x.Marked("EntryEditText"), "quero pedir uma pizza");
            app.PressEnter();
            app.Tap(x => x.Marked("CachedImageView"));
            app.Screenshot("Tapped on view with class: CachedImageView");

#if DEBUG
#endif
        }
    }
}
