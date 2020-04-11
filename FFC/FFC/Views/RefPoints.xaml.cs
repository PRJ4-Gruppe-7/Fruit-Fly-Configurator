using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFC.Models;
using FFC.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FFC.Views
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefPoints : ContentPage
    {
        RefPointsViewModel vm;
        public RefPoints()
        {
            InitializeComponent();
            vm = new RefPointsViewModel();
            listViewPoints.ItemsSource = vm.Points;
        }

        async void GetRefPoints_OnClicked(object sender, EventArgs e)
        {
            //List<Reference> Items = new List<Reference>();
            //var vm = (RefPointsViewModel)this.BindingContext;
            //vm.Items = await App.refPointManager.GetRefPointsAsync();
        }
    }
}