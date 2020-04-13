using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using FFC.Models;
using Xamarin.Forms;

namespace FFC.ViewModels
{
    public class RefPointsViewModel : BaseViewModel
    {
        public RefPointsViewModel()
        {
            Title = "Uploaded Reference Points";
            GetRefPointsCommand = new Command(GetRefPoints);
        }

        #region Properties
        public ObservableCollection<Reference> refs = new ObservableCollection<Reference>();
        public ObservableCollection<Reference> Refs { get { return refs; } }

        #endregion

        #region Commands

        public ICommand GetRefPointsCommand { get; }

        #endregion

        #region Methods

        public async void GetRefPoints()
        {
            //var vm = (RefPointsViewModel)this.BindingContext;
            //vm.Points = await App.refPointManager.GetRefPointsAsync();

            #region Test
            /*  Test for "Get Reference Point" button, where it is displayed in the console
             *  That it retrieves the second point in the database with the ID 2, with the 
             *  values x:81, y:23 and rssi1: 80
             */

            refs.Clear();
            var tempRefs = await App.refPointManager.GetRefPointsAsync();

            for (int i = 0; i < tempRefs.Count; i++)
            {
                refs.Add(new Reference { referencepointId = tempRefs[i].referencepointId, x = tempRefs[i].x, y = tempRefs[i].y, rssI1 = tempRefs[i].rssI1 });
            }

            for (int i = 0; i < Refs.Count; i++)
            {
                Console.WriteLine("ID: {0}, x: {1}, y: {2}, rssi: {3}", Refs[i].referencepointId, Refs[i].x, Refs[i].y, Refs[i].rssI1);
            }

            Console.WriteLine("{0}", Refs.Count);
            NotifyPropertyChanged("Refs");

            //int i = 0;
            //while(Refs[i] != null)
            //{
            //    RefList.Add(string.Format("ID: {0} X: {1} Y: {2}", i+1, Refs[i].x, Refs[i].y));
            //    i++;
            //}
            // Console.WriteLine("{0}, {1}, {2}", Points[1].x, Points[1].y, Points[1].rssI1);
            #endregion
        }

        #endregion

    }
}
