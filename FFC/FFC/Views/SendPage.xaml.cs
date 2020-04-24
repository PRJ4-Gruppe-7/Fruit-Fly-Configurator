using FFC.Models;
using FFC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using FFC.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.NetworkInformation;

namespace FFC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPage : ContentPage
    {
        public SendPage()
        {
            InitializeComponent();
        }

        private void InfoClicked(object sender, EventArgs e)
        {
            DisplayAlert("Information", "X/Y unit is meter.\n0/0 is the bottom left of any given floor plan", "OK");
        }

        //async void Button_Clicked(object sender, EventArgs e)
        //{
        //    /*
        //     *      Connecting to sniffer and receiving rssi value for this device
        //     */

        //    //var tempRssi = AsynchronousClient.StartClient();

        //    var spvm = (SendPageViewModel)this.BindingContext;
        //    var item = new Reference();

        //    item.x = Int32.Parse(spvm.XValue);
        //    item.y = Int32.Parse(spvm.YValue);
        //    item.rssI1 = Int32.Parse(spvm.RSSIValue); //Comment in tempRssi and set as parameter for this function

        //    await App.refPointManager.PostRefPointAsync(item);
        //    await Navigation.PopAsync();
        //}
    }
}