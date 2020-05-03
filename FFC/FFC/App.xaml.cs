using FFC.Services;
using FFC.Services.WebSocketService;
using FFC.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FFC
{
    public partial class App : Application
    {
        public static RefPointManager refPointManager { get; private set; }
        public static WebSocketService webSocketService { get; private set; }
        public static WebSocketManager webSocketManager { get; private set; }

        public static SnifferSource[] sources =
        {
            new SnifferSource {name = "SNF1", hostname = "", numericHostName = "123.456.789.123", port = 27015},
        };

        public App()
        {
            InitializeComponent();
            refPointManager = new RefPointManager(new RestApiService());
            webSocketService = new WebSocketService(new ASyncSocket(sources[0].numericHostName, sources[0].port));
            webSocketManager = new WebSocketManager(webSocketService);
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
