using FFC.Services;
using FFC.Services.WebSocketService;
using FFC.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FFC
{
    public partial class App : Application
    {
        public static RestApiManager restApiManager { get; private set; }
        public static WebSocketService webSocketService { get; private set; }
        public static WebSocketManager webSocketManager { get; private set; }

        public static SnifferSource[] sources =
        {
            new SnifferSource { name = "SNF2_Daniel", hostname ="", numericHostName = "192.168.0.136", port = 27015},
            new SnifferSource { name = "SNF1_Mathias", hostname ="", numericHostName = "192.168.0.137", port = 27015},
            new SnifferSource { name = "SNF3_Viktor", hostname ="", numericHostName = "192.168.0.138", port = 27015},
        };

        public App()
        {
            InitializeComponent();
            restApiManager = new RestApiManager(new RestApiService());

            WebSocketService webSocketService;

            var templist = new List<ASyncSocket>();

            for (int i = 0; i < sources.Length; i++)
            {
                templist.Add(new ASyncSocket(sources[i].numericHostName, sources[i].port));
               
            }
            webSocketService = new WebSocketService(templist);
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
