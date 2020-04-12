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
            listViewPoints.ItemsSource = vm.Refs;
            //vm.Refs.Add(new Reference { referencepointId = 267, x = 1, y = 2, rssI1 = 3 });
            vm.GetRefPoints();
        }

        //async void GetRefPoints_OnClicked(object sender, EventArgs e)
        //{
        //    //var vm = (RefPointsViewModel)this.BindingContext;
        //    //vm.Points = await App.refPointManager.GetRefPointsAsync();

        //    #region Test
        //    /*  Test for "Get Reference Point" button, where it is displayed in the console
        //     *  That it retrieves the second point in the database with the ID 2, with the 
        //     *  values x:81, y:23 and rssi1: 80
        //     */
        //    var Points = await App.refPointManager.GetRefPointsAsync();
        //    // Console.WriteLine("{0}, {1}, {2}", Points[1].x, Points[1].y, Points[1].rssI1);

        //    int i = 0;

        //    while(Points[i] != null)
        //    {
        //        Reference tempRef = new Reference()
        //    }
        //    #endregion
        //}

        async void DeleteRef_Clicked(object sender, EventArgs e)
        {
            //var vm = (RefPointsViewModel)this.BindingContext;
            //await App.refPointManager.DeleteReferenceAsync(vm.CurrentID);

            #region Test
            /*  Test for "Delete" button, where the ref point with ID X will be deleted
             *  in the database. This can be checked on https://fruitflyapi.azurewebsites.net/index.html
             *  The ID parameter has to be changed for each time the delete button is pressed, 
             *  since it will be deleted permanently from the DB. First we check if the ID is in the
             *  database and print it out in the console. Then we delete it. Afterwards we check again
             *  if it is in the database, and print it out in the console. 
             */

            //Coordinate with ID to be deleted (Check wether or not the ID chosen is in the database.
            //If it is not, an exception will be catched and displayed in the console.
            string ID = "252";

            //Check for the specific ID and print in console
            var Points1 = await App.refPointManager.GetSpecificRefPointAsync(ID);
            Console.WriteLine("{0}, {1}, {2}", Points1.x, Points1.y, Points1.rssI1);

            //Delete the ID
            await App.refPointManager.DeleteReferenceAsync(ID);

            //Check for the specific ID and print in console
            try
            {
                var Points2 = await App.refPointManager.GetSpecificRefPointAsync(ID);
                Console.WriteLine("{0}, {1}, {2}", Points2.x, Points2.y, Points2.rssI1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception message: {0} ", ex.Message);
            }
            #endregion
        }
    }
}