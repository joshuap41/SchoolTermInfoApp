using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SchoolTermInfoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            //Creates a navigation for use in iOS
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
