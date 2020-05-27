using System;
using System.Windows.Input;
using FFC.Models;
using Prism.Commands;

namespace FFC.ViewModels
{
    public class SendPageViewModel : BaseViewModel
    {
        private Reference _currentReference;

        public SendPageViewModel()
        {
            _currentReference = new Reference();
            Title = "Send Reference Points";
        }

        #region CurrentReference

        public Reference CurrentReference
        {
            get { return _currentReference; }
            set
            {
                if (value != _currentReference)
                {
                    _currentReference = value;
                    NotifyPropertyChanged(nameof(CurrentReference));
                }
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
                CurrentReference.x++;
                NotifyPropertyChanged(nameof(CurrentReference));
            }

            if (value == "y")
            {
                CurrentReference.y++;
                NotifyPropertyChanged(nameof(CurrentReference));
            }
        }

        ICommand _decrementXCommand;
        public ICommand DecrementXCommand => _decrementXCommand ?? (_decrementXCommand = 
            new DelegateCommand(DecrementXCommandExecute, DecrementXCommandCanExecute).ObservesProperty(() => CurrentReference));

        bool DecrementXCommandCanExecute()
        { return CurrentReference.x > 0 ? true : false; }


        void DecrementXCommandExecute()
        {
            CurrentReference.x--;
            NotifyPropertyChanged(nameof(CurrentReference));
        }

        ICommand _decrementYCommand;
        public ICommand DecrementYCommand => _decrementYCommand ?? (_decrementYCommand =
            new DelegateCommand(DecrementYCommandExecute, DecrementYCommandCanExecute).ObservesProperty(() => CurrentReference));

        bool DecrementYCommandCanExecute()
        { return CurrentReference.y > 0 ? true : false; }

        void DecrementYCommandExecute()
        {
            CurrentReference.y--;
            NotifyPropertyChanged(nameof(CurrentReference));
        }

        ICommand _sendRefCommand;
        public ICommand SendRefCommand
        {
            get { return _sendRefCommand ?? (_sendRefCommand = new DelegateCommand(SendRefCommandExecuteAsync)); }
        }

        async void SendRefCommandExecuteAsync()
        {
            Reference refItem = new Reference();

            try
            {
                //App.webSocketManager.InitiateClient();
                //App.webSocketManager.ReceiveAndProcessResponse();
                //refItem = App.webSocketManager.CreateDataInstance();
                refItem.x = CurrentReference.x;
                refItem.y = CurrentReference.y;
                await App.restApiManager.PostRefPointAsync(refItem);
            }

            catch(ArgumentException arg)
            { Console.WriteLine($"{arg} - Server possibly down");
                App.webSocketManager.ShutdownClient();
            }
        }
        #endregion
    }
}