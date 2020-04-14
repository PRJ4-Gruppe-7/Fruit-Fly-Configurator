using System;
using System.Windows.Input;
using FFC.Models;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using FFC.Services;
using Prism.Commands;
using Prism.Navigation.Xaml;

namespace FFC.ViewModels
{
    public class SendPageViewModel : BaseViewModel
    {
        public SendPageViewModel()
        {
            Title = "Send Reference Points";
            IncrementCommand = new Command<string>(Increment);
        }

        #region Properties

        public int _xValue { get; set; }
        public string XValue
        {
            get { return $"{_xValue}"; }
            set { 
                try
                {
                    _xValue = value.Length > 0 ? Int32.Parse(value) : _xValue;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Exception: {ex}");
                }
                
                }
        }

        public int _yValue { get; set; }
        public string YValue
        {
            get { return $"{_yValue}"; }
            set {
                try 
                {
                    _yValue = value.Length > 0 ? Int32.Parse(value) : _yValue;
                }
                 catch(Exception ex)
                {
                    Console.WriteLine($"Exception: {ex}");
                }
            }
        }

        public int _rssi { get; set; }
        public string RSSIValue
        {
            get { return $"{_rssi}"; }
            set { _rssi = Int32.Parse(value); }
        }
        
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
                NotifyPropertyChanged(nameof(XValue));
            }

            if (value == "y")
            {
                _yValue++;
                NotifyPropertyChanged(nameof(YValue));
            }
        }

        void DecrementX()
        {
            _xValue--;
            NotifyPropertyChanged(nameof(XValue));
        }

        bool DecrementXCommandCanExecute()
        { return _xValue > 0 ? true : false; }

        void DecrementY()
        {
            _yValue--;
            NotifyPropertyChanged(nameof(YValue));
        }

        bool DecrementYCommandCanExecute()
        { return _yValue > 0 ? true : false; }


        #endregion
    }
}
