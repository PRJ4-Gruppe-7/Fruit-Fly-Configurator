using FFC.Models;
using FFC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            DisplayAlert("Information", "X/Y unit is meter\n0/0 is the bottom left of any given floor plan", "OK");
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            //StartClient();
            //var item = (Reference)BindingContext;
            var item = new Reference();
            item.x = 1;
            item.y = 3;
            item.rssI1 = 1;
            await App.refPointManager.PostRefPointAsync(item);
            await Navigation.PopAsync();
        }
    }
}