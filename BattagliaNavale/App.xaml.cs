using BattagliaNavale.Views;

namespace BattagliaNavale
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Home());
        }

        protected override void OnSleep()
        {
            base.OnSleep();

            MainPage.Stop
        }
    }
}
