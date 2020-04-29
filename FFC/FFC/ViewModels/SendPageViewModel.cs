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

        //MAC Address for this device
        private static string _macAddress = NetworkInterface.GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up)
            .Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();

        private static int Sniffer_Count = 3;

        private static SnifferSource[] sources =
        {
            new SnifferSource {name = "SNF1", hostname = "", numericHostName = "123.456.789.123", port = 27015},
        };

        ASyncSocket[] sockets = new ASyncSocket[Sniffer_Count];
        List<List<int>> RSSIList = new List<List<int>>();
        IDictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        private static Random rng = new Random(Guid.NewGuid().GetHashCode());
        private static string RandomRSSIString()
        {
            return String.Format("7.192.163.51:{0}, 199.187.194.244:{1}, 6.38.202.48:{2}, 2.55.101.44:{3}", rng.Next(0, 100), rng.Next(0, 100), rng.Next(0, 100), rng.Next(0,100));
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

            for (int i = 0; i < Sniffer_Count; i++)
                {
                    //sockets[i].Send("RETR test.txt");
                    //sockets[i].Receive();

                    //For testing purpose. Fills response for sockets.
                    sockets[i].response = RandomRSSIString().Split(',');

                    for (int j = 0; j < sockets[i].response.Length; j++)
                    {
                        var thisItem = sockets[i].response[j].Split(':');
                        try
                        {
                            // Try adding the ip as a key, into the dictionary.
                            // If successful, create the list containing the received signal strength values
                            dict.Add(thisItem[0], new List<string>() { thisItem[1] });
                        }
                        catch (ArgumentException)
                        {
                            // If the key already exist in the dictionary, we add the new rssi value
                            // to the list linked to that key.
                            dict[thisItem[0]].Add(thisItem[1]);
                        }
                    }
                }


            // Make the list of RSSI values.
            foreach (KeyValuePair<string, List<string>> p in dict)
            {
                RSSIList.Add(p.Value.Select(int.Parse).ToList());
            }

            foreach (var item in RSSIList)
            {
                Console.WriteLine("Item {0}",item);
                foreach (var i in item)
                {
                    Console.Write("{0},", i);
                }
                Console.WriteLine("");
            }

            RSSIList.Clear();
            dict.Clear();

            //var item = new Reference();

            //item.x = Int32.Parse(XValue);
            //item.y = Int32.Parse(YValue);
            //item.rssI1 = Int32.Parse(RSSIValue); 

            //await App.refPointManager.PostRefPointAsync(item);
        }
        #endregion
    }
}
