using System;
using System.Windows.Input;
using FFC.Models;
using FFC.Utilities;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using Prism.Commands;

namespace FFC.ViewModels
{
    public class SendPageViewModel : INotifyPropertyChanged
    {
        public SendPageViewModel()
        {
            IncrementCommand = new Command<string>(Increment);
            SendRefCommand = new Command(SendRef);
        }

        #region Properties

        public string XValue
        {
            get { return $"{_xValue}"; }
            set { _xValue = value.Length > 0 ? Int32.Parse(value) : _xValue; }
        }

        public string YValue
        {
            get { return $"{_yValue}"; }
            set { _yValue = value.Length > 0 ? Int32.Parse(value) : _yValue; }
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
        
        ICommand _decrementXCommand;
        public ICommand DecrementXCommand => _decrementXCommand ?? (_decrementXCommand = 
            new DelegateCommand(DecrementX, DecrementXCommandCanExecute).ObservesProperty(() => XValue));

        ICommand _decrementYCommand;
        public ICommand DecrementYCommand => _decrementYCommand ?? (_decrementYCommand =
            new DelegateCommand(DecrementY, DecrementYCommandCanExecute).ObservesProperty(() => YValue));

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
        }

        void DecrementX()
        {
            _xValue--;
            OnPropertyChanged(nameof(XValue));
        }

        bool DecrementXCommandCanExecute()
        { return _xValue > 0 ? true : false; }

        void DecrementY()
        {
            _yValue--;
            OnPropertyChanged(nameof(YValue));
        }

        bool DecrementYCommandCanExecute()
        { return _yValue > 0 ? true : false; }

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
