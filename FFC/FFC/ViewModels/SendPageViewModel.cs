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
        }

        #region Properties

        public int _xValue { get; set; }
        public string XValue
        {
            get { return $"{_xValue}"; }
            set { 
                try
                {
                    _xValue = Int32.Parse(value) >= 0 ? Int32.Parse(value) : _xValue;
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
                    _yValue = Int32.Parse(value) >= 0 ? Int32.Parse(value) : _yValue;
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
            set
            {
                try { _rssi = Int32.Parse(value); }
                catch (Exception ex) { Console.WriteLine($"Exception: {ex}"); }
            }
        }

        #endregion

        #region Commands

        ICommand _incrementCommand;
        public ICommand IncrementCommand 
        { 
            get { return _incrementCommand ?? (_incrementCommand = new DelegateCommand<string>(IncrementCommandExecute)); } 
        }
        
        void IncrementCommandExecute(string value)
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

        ICommand _decrementXCommand;
        public ICommand DecrementXCommand => _decrementXCommand ?? (_decrementXCommand = 
            new DelegateCommand(DecrementXCommandExecute, DecrementXCommandCanExecute).ObservesProperty(() => XValue));

        bool DecrementXCommandCanExecute()
        { return _xValue > 0 ? true : false; }


        void DecrementXCommandExecute()
        {
            _xValue--;
            NotifyPropertyChanged(nameof(XValue));
        }

        ICommand _decrementYCommand;
        public ICommand DecrementYCommand => _decrementYCommand ?? (_decrementYCommand =
            new DelegateCommand(DecrementYCommandExecute, DecrementYCommandCanExecute).ObservesProperty(() => YValue));

        bool DecrementYCommandCanExecute()
        { return _yValue > 0 ? true : false; }

        void DecrementYCommandExecute()
        {
            _yValue--;
            NotifyPropertyChanged(nameof(YValue));
        }

        ICommand _sendRefCommand;
        public ICommand SendRefCommand
        {
            get { return _sendRefCommand ?? (_sendRefCommand = new DelegateCommand(SendRefCommandExecute)); }
        }

        async void SendRefCommandExecute()
        {
            var item = new Reference();

            item.x = Int32.Parse(XValue);
            item.y = Int32.Parse(YValue);
            item.rssI1 = Int32.Parse(RSSIValue); //Comment in tempRssi and set as parameter for this function

            await App.refPointManager.PostRefPointAsync(item);
            //await Navigation.PopAsync();
        }
        #endregion
    }
}
