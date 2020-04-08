using FFC.Services;
using FFC.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FFC
{
    public partial class App : Application
    {
        public static RefPointManager refPointManager { get; private set; }
        

        public App()
        {
            InitializeComponent();
            //StartClient();
            refPointManager = new RefPointManager(new RestApiService());
            MainPage = new MainPage();
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
