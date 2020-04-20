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
            refs.Clear();
            var tempRefs = await App.refPointManager.GetRefPointsAsync();

            for (int i = 0; i < tempRefs.Count; i++)
            {
                refs.Add(new Reference { referencepointId = tempRefs[i].referencepointId, x = tempRefs[i].x, y = tempRefs[i].y, rssI1 = tempRefs[i].rssI1 });
            }

            NotifyPropertyChanged(nameof(Refs));
        }

        public async void DeleteRefPoint()
        {
            string id = CurrentID.ToString();
            await App.refPointManager.DeleteReferenceAsync(id);
            //var index = Refs.IndexOf(item);
            //Refs.RemoveAt(index);
            //NotifyPropertyChanged(nameof(Refs));
        }

        public async void DeleteAllRefPoints()
        {
            await App.refPointManager.DeleteAllRefPointsAsync();

            //Reseeding ID-count on deletion
            await App.refPointManager.PutRefPointAsync();
        }
        #endregion
    }
}
