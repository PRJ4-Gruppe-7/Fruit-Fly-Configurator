using FFC.Models;
using FFC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Services.WebSocketService
{
    public class WebSocketService : IWebsocketService
    {
        string _mac;
        private static int Sniffer_Count = 3;
        private static int MEAN = 5;

        IASyncSocket ASyncSocket;

        //Former ASyncSocket
        ASyncSocket[] sockets = new ASyncSocket[Sniffer_Count];

        List<List<int>> average = new List<List<int>>();
        List<int> meanlist = new List<int> { 0, 0, 0 };
        IDictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        public SendPageViewModel BindingContext { get; private set; }
        private static Random rng = new Random(Guid.NewGuid().GetHashCode());


        private static string RandomRSSIString()
        {
            return String.Format("CE:86:B6:23:33:C6;{0},199.187.194.244;{1},76:90:38:19:D5:04;{2},CE:C6:A1:6B:E8:98;{3}", rng.Next(0, 100), rng.Next(0, 100), rng.Next(0, 100), rng.Next(0, 100));
        }

        public WebSocketService(IASyncSocket socket)
        {
            ASyncSocket = socket;
            
        }

        public void InitiateClient()
        {
            for (int i = 0; i < Sniffer_Count; i++)
            {
                sockets[i] = (ASyncSocket)ASyncSocket;
                //ASyncSocket.StartClient();
            }
        }

        public void ReceiveResponse()
        {
            for (int k = 0; k < MEAN; k++)
            {
                for (int i = 0; i < Sniffer_Count; i++)
                {
                    //sockets[i].Send("RETR test.txt");
                    //sockets[i].Receive();


                    //sockets[i].response.Split(',');

                    //For testing purpose. Fills response for sockets. Delete later.
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

                    //sockets[i].ShutdownClient();
                }

                GetMacAddress();
                average.Add(dict[_mac].Select(int.Parse).ToList());
                dict.Clear();
            }

            for (int i = 0; i < meanlist.Count; i++)
            {
                meanlist[i] = 0;
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
            average.Clear();
        }

        public Reference CreateDataInstance()
        {
            Reference refItem = new Reference
            {
                rssI1 = meanlist[0] / MEAN,
                rssI2 = meanlist[1] / MEAN,
                rssI3 = meanlist[2] / MEAN
            };
            
            //meanlist.Select(x => x = 0).ToList();

            return refItem;
        }


        //Retrieves mac address for this device
        private void GetMacAddress()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                _mac = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes()).Replace('-', ':');

                Console.WriteLine($"{_mac}");
                break;
            }
        }

    }
}
