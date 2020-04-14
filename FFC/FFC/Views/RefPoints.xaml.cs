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
        Reference tempref = new Reference();
        public RefPoints()
        {
            InitializeComponent();
            vm = new RefPointsViewModel();
            vm.GetRefPoints();
            listViewPoints.ItemsSource = vm.Refs;
            this.BindingContext = vm;
        }

        void GetRefPoints_OnClicked(object sender, EventArgs e)
        {
            vm.GetRefPoints();
            listViewPoints.ItemsSource = vm.Refs;
        }
        void DeleteRef_Clicked(object sender, EventArgs e)
        {
            vm.DeleteRefPoint();
        }

        private void listViewPoints_ItemTapped(object sender, ItemTappedEventArgs e)
        { 
            tempref = (e.Item as Reference);
            vm.CurrentID = tempref.referencepointId;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Information", "Upon deletion of a reference point,\nmake sure to update the table", "OK");
        }
    }
}