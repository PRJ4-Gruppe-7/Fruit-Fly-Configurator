using System;
using System.Windows.Input;
using FFC.Models;
using FFC.Utilities;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace FFC.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            IncrementCommand = new Command<string>(Increment);
            DecrementCommand = new Command<string>(Decrement);
        }

        #region Properties

        public string XValue
        {
            get { return $"{_xValue}"; }
            set { _xValue = Int32.Parse(value); }
        }

        public string YValue
        {
            get { return $"{_yValue}"; }
            set { _yValue = Int32.Parse(value); }
        }

        public string RSSIValue
        {
            get { return $"{_rssi}"; }
            set { _rssi = Int32.Parse(value); }
        }

        private int _xValue { get; set; }
        private int _yValue { get; set; }
        private int _rssi { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion

        #region Commands

        public ICommand IncrementCommand { get; }
        public ICommand DecrementCommand { get; }

        #endregion

        #region Methods

        void Increment(string value)
        {
            if (value == "x")
            {
                _xValue++;
                OnPropertyChanged(nameof(XValue));
            }

            if (value == "y")
            {
                _yValue++;
                OnPropertyChanged(nameof(YValue));
            }
            //else
            //    throw new ArgumentException("Wrong value to increment");
        }

        void Decrement(string value)
        {
            if (value == "x")
            {
                _xValue--;
                OnPropertyChanged(nameof(XValue));
            }

            if (value == "y")
            {
                _yValue--;
                OnPropertyChanged(nameof(YValue));
            }
            //else
              //  throw new ArgumentException("Wrong value to decrement");
        }

        //Test for SQLite
        public void AddReferencePointToDatabase(int x, int y, int rssi)
        {
            using (var db = new ReferenceContext())
            {
                db.Add(new Reference { xPoint = x, yPoint = y, RSSI = rssi });
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        #endregion
    }
}
