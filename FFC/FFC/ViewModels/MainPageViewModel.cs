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
            DecrementCommand = new Command<string>(Decrement, DecrementCommandCanExecute);
            SendRefCommand = new Command(SendRef);
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
        public ICommand SendRefCommand { get; }

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

        bool DecrementCommandCanExecute(string value)
        {
            if (value == "x")
                return _xValue <= 0 ? false : true;

            //if (value == "y")
            else
                return _yValue <= 0 ? false : true;
        }

        //Test for SQLite
        public void SendRef()
        {
            _rssi = new Random().Next(1, 1000);

            using (var db = new ReferenceContext())
            {
                db.Add(new Reference { xPoint = _xValue, yPoint = _yValue, RSSI = _rssi });
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        #endregion
    }
}
