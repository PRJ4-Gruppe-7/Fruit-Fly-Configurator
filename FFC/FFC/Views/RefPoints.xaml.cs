﻿using System;
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

        }

        private void GetRefPoints_OnClicked(object sender, EventArgs e)
        {
            vm.GetRefPointsAsync();
            listViewPoints.ItemsSource = vm.Refs;
        }
        private async void DeleteRefAsync_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning", $"Are you sure you want to delete reference point {vm.CurrentID} from list? This cannot be undone.", "Yes", "No");

            if (answer)
                vm.DeleteRefPointAsync();
        }

        private void listViewPoints_ItemTapped(object sender, ItemTappedEventArgs e)
        { 
            tempref = (e.Item as Reference);
            vm.CurrentID = tempref.referencepointId;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Information", "Get - retrieves the list of reference points.\n" +
                            "Delete - deletes highlighted reference point.\n" +
                            "Reset - deletes all reference points in list.", "OK");
        }

        private async void ResetAsync_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning", "Are you sure you want to delete all reference points from list? This cannot be undone.", "Yes", "No");
            
            if(answer)
                vm.DeleteAllRefPointsAsync();
        }
    }
}