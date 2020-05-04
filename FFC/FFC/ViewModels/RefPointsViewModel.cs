using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using FFC.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FFC.ViewModels
{
    public class RefPointsViewModel : BaseViewModel
    {
        private ObservableCollection<Reference> refs = new ObservableCollection<Reference>();
        int _currentId = 0;
   
        public RefPointsViewModel()
        {
            Title = "Uploaded Reference Points";
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

        #endregion

        #region Methods

        public async void GetRefPoints()
        {
            refs.Clear();
            
            var tempRefs = await App.refPointManager.GetRefPointsAsync();

            for (int i = 0; i < tempRefs.Count; i++)
            {
                refs.Add(new Reference { referencepointId = tempRefs[i].referencepointId, x = tempRefs[i].x, y = tempRefs[i].y, rssI1 = tempRefs[i].rssI1, rssI2 = tempRefs[i].rssI2, rssI3 = tempRefs[i].rssI3 });
            }

            NotifyPropertyChanged(nameof(Refs));
        }

        public async void DeleteRefPoint()
        {
            string id = CurrentID.ToString();
            await App.refPointManager.DeleteReferenceAsync(id);

            GetRefPoints();
        }

        public async void DeleteAllRefPoints()
        {
            await App.refPointManager.DeleteAllRefPointsAsync();

            //Reseeding ID-count on deletion
            await App.refPointManager.PutRefPointAsync();

            GetRefPoints();
        }
        #endregion
    }
}
