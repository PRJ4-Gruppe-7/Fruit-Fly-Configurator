using System;
using System.Windows.Input;
using FFC.Models;
using Prism.Commands;

namespace FFC.ViewModels
{
    public class SendPageViewModel : BaseViewModel
    {
        private Coordinates _currentCoordinates;

        public SendPageViewModel()
        {
            _currentCoordinates = new Coordinates();
            Title = "Send Reference Points";
        }

        #region CurrentCoordinates

        public Coordinates CurrentCoordinates
        {
            get { return _currentCoordinates; }
            set
            {
                if (value != _currentCoordinates)
                {
                    _currentCoordinates = value;
                    NotifyPropertyChanged(nameof(CurrentCoordinates));
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
                CurrentCoordinates.XValue++;
                NotifyPropertyChanged(nameof(CurrentCoordinates));
            }

            if (value == "y")
            {
                CurrentCoordinates.YValue++;
                NotifyPropertyChanged(nameof(CurrentCoordinates));
            }
        }

        ICommand _decrementXCommand;
        public ICommand DecrementXCommand => _decrementXCommand ?? (_decrementXCommand = 
            new DelegateCommand(DecrementXCommandExecute, DecrementXCommandCanExecute).ObservesProperty(() => CurrentCoordinates));

        bool DecrementXCommandCanExecute()
        { return CurrentCoordinates.XValue > 0 ? true : false; }


        void DecrementXCommandExecute()
        {
            CurrentCoordinates.XValue--;
            NotifyPropertyChanged(nameof(CurrentCoordinates));
        }

        ICommand _decrementYCommand;
        public ICommand DecrementYCommand => _decrementYCommand ?? (_decrementYCommand =
            new DelegateCommand(DecrementYCommandExecute, DecrementYCommandCanExecute).ObservesProperty(() => CurrentCoordinates));

        bool DecrementYCommandCanExecute()
        { return CurrentCoordinates.YValue > 0 ? true : false; }

        void DecrementYCommandExecute()
        {
            CurrentCoordinates.YValue--;
            NotifyPropertyChanged(nameof(CurrentCoordinates));
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
                App.webSocketManager.InitiateClient();
                App.webSocketManager.ReceiveAndProcessResponse();
                refItem = App.webSocketManager.CreateDataInstance();
                //refItem.x = CurrentCoordinates.XValue;
                //refItem.y = CurrentCoordinates.YValue;
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