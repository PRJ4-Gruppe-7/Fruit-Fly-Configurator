using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FFC.Models;
using FFC.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FFC.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        { }

        #region Properties

        public string XValue 
        { 
            get { return _xValue.ToString(); }
            private set { } 
        }
        public string YValue 
        {
            get { return _yValue.ToString(); } 
            private set { }
        }
        public string RSSI
        { 
            get { return _rssi.ToString(); }
            private set { }
        }

        private int _xValue { get; set; }
        private int _yValue { get; set; }
        private int _rssi { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region DelegateCommand
        public ICommand IncrementCommand
        {
            get
            {
                return new Command<char>((x) => Increment(x));
            }
        }

        //public ICommand IncrementCommand
        //{
        //    get
        //    {
        //        return _IncrementCommand ?? (_IncrementCommand = new
        //            DelegateCommand<char>(IncrementCommandExecute));
        //    }
        //}
        #endregion

        #region Methods
        void Increment(char value)
        {
            if (value == 'x')
            {
                _xValue++;
                OnPropertyChanged("XValue");
            }
            if (value == 'y')
            {
                _yValue++;
                OnPropertyChanged("YValue");
            }
        }

        //Test for SQLite
        public void AddReferencePointToDatabase(int x, int y, int rssi)
        {
            using (var db = new ReferenceContext())
            {
                db.Add(new Reference { xPoint = x, yPoint = y, RSSI = rssi });
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
