using System;
using System.Collections.Generic;
using System.Windows.Input;
using FFC.Models;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using FFC.Services;
using Prism.Commands;
using Prism.Navigation.Xaml;

namespace FFC.ViewModels
{
    public class SendPageViewModel : BaseViewModel
    {
        #region WebSocketConfig

        string _mac;

        private static int MEAN = 5;
        private static int Sniffer_Count = 3;
        private static Random rng = new Random(Guid.NewGuid().GetHashCode());

        List<List<int>> average = new List<List<int>>();
        List<int> meanlist = new List<int> { 0, 0, 0 };
        IDictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        ASyncSocket[] sockets = new ASyncSocket[Sniffer_Count];


        private static SnifferSource[] sources =
        {
            new SnifferSource {name = "SNF1", hostname = "", numericHostName = "123.456.789.123", port = 27015},
        };

        private static string RandomRSSIString()
        {
            return String.Format("76:90:38:19:D5:04;{0}, 199.187.194.244;{1}, 6.38.202.48;{2},7.192.163.51;{3}", rng.Next(0, 100), rng.Next(0, 100), rng.Next(0, 100), rng.Next(0,100));
        }
        #endregion


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


            for (int i = 0; i < Sniffer_Count; i++)
            {
                ASyncSocket s = new ASyncSocket(sources[0].numericHostName, sources[0].port);
                sockets[i] = s;
                //s.StartClient();
            }

            for (int k = 0; k < MEAN; k++)
            {
                for (int i = 0; i < Sniffer_Count; i++)
                {
                    //sockets[i].Send("RETR test.txt");
                    //sockets[i].Receive();


                    //sockets[i].response.Split(',');

                    //For testing purpose. Fills response for sockets.
                    sockets[i].response = RandomRSSIString().Split(',');

                    for (int j = 0; j < sockets[i].response.Length; j++)
                    {
                        var thisItem = sockets[i].response[j].Split(';');
                        try
                        {
                            dict.Add(thisItem[0], new List<string>() { thisItem[1] });
                        }
                        catch (ArgumentException)
                        {
                            dict[thisItem[0]].Add(thisItem[1]);
                        }
                    }
                }

                foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    _mac = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes()).Replace('-', ':');

                    Console.WriteLine($"{_mac}");
                    break;
                }

                average.Add(dict[_mac].Select(int.Parse).ToList());
                dict.Clear();
            }

            //Extracts values from nested average list for each sniffer 
            //to accumulate sniffer values and in the end divide by values collected
            if (average.Count == MEAN)
            {
                foreach (var l in average)
                {
                    for (int i = 0; i < l.Count; i++)
                    {
                        meanlist[i] += l[i];
                    }
                }
            }

            var refItem = new Reference();

            refItem.rssI1 = meanlist[0] / MEAN;
            refItem.rssI2 = meanlist[1] / MEAN;
            refItem.rssI3 = meanlist[2] / MEAN;
            refItem.x = Int32.Parse(XValue);
            refItem.y = Int32.Parse(YValue);

            average.Clear();
            meanlist.Clear();

            //await App.refPointManager.PostRefPointAsync(refItem);
        }
        #endregion
    }
}
