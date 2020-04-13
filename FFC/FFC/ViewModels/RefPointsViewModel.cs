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
        private ObservableCollection<Reference> refs = new ObservableCollection<Reference>();
        int _currentId = 0;
        
        public RefPointsViewModel()
        {
            Title = "Uploaded Reference Points";
            GetRefPointsCommand = new Command(GetRefPoints);
        }

        #region Properties
        
        public ObservableCollection<Reference> Refs 
        { 
            get { return refs; } 
            set { refs = value; NotifyPropertyChanged(nameof(Refs)); }
        }


        public int CurrentID
        {
            get => _currentId;
            set => _currentId = value;
        }

        #endregion

        #region Commands

        public ICommand GetRefPointsCommand { get; }
        //ICommand _getRefPointsXCommand;
        //public ICommand GetRefPointsCommand => _getRefPointsXCommand ?? (_getRefPointsXCommand =
        //    new DelegateCommand(GetRefPoints).ObservesProperty(() => Refs));

        #endregion

        #region Methods

        public async void GetRefPoints()
        {
            //var vm = (RefPointsViewModel)this.BindingContext;
            //vm.Points = await App.refPointManager.GetRefPointsAsync();

            refs.Clear();
            var tempRefs = await App.refPointManager.GetRefPointsAsync();

            for (int i = 0; i < tempRefs.Count; i++)
            {
                refs.Add(new Reference { referencepointId = tempRefs[i].referencepointId, x = tempRefs[i].x, y = tempRefs[i].y, rssI1 = tempRefs[i].rssI1 });
            }

            NotifyPropertyChanged(nameof(Refs));

            #region Test
            /*  Test for "Get Reference Point" button, where it is displayed in the console
             *  That it retrieves the second point in the database with the ID 2, with the 
             *  values x:81, y:23 and rssi1: 80
             */

            for (int i = 0; i < Refs.Count; i++)
            {
                Console.WriteLine("ID: {0}, x: {1}, y: {2}, rssi: {3}", Refs[i].referencepointId, Refs[i].x, Refs[i].y, Refs[i].rssI1);
            }

            Console.WriteLine("Refs: {0}", Refs.Count);
            Console.WriteLine("refs: {0}", refs.Count);

            #endregion
        }

        #endregion

    }
}
