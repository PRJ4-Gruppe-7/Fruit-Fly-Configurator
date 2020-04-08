using FFC.Models;
using FFC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var item = (Reference)BindingContext;
            await App.refPointManager.PostRefPointAsync(item);
            await Navigation.PopAsync();
        }
    }
}