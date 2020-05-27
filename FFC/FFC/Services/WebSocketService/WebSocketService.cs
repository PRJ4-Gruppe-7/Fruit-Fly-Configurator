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

        IASyncSocket ASyncSocket;

        //Former ASyncSocket
        List<ASyncSocket> sockets = new List<ASyncSocket>(Sniffer_Count);

        //List<List<int>> average = new List<List<int>>();
        List<Double> meanlist = new List<Double> { 0, 0, 0 };
        IDictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        public SendPageViewModel BindingContext { get; private set; }
        private static Random rng = new Random(Guid.NewGuid().GetHashCode());


        private static string RandomRSSIString()
        {
            return String.Format("CE:86:B6:23:33:C6;{0},199.187.194.244;{1},76:90:38:19:D5:04;{2},CE:C6:A1:6B:E8:98;{3}", rng.Next(0, 100), rng.Next(0, 100), rng.Next(0, 100), rng.Next(0, 100));
        }

        public WebSocketService(List<ASyncSocket> socket)
        {
            sockets = socket;
            
        }

        public void InitiateClient()
        {
            for (int i = 0; i < Sniffer_Count; i++)
            {
                sockets[i] = (ASyncSocket)ASyncSocket;
                System.Threading.Thread.Sleep(1000);
                sockets[i].StartClient();
            }
        }

        public void ReceiveResponse()
        {
            for (int i = 0; i < Sniffer_Count; i++)
            {
                sockets[i].Send("RETR SnifferData.txt");
                System.Threading.Thread.Sleep(1000);
                sockets[i].Receive();
                System.Threading.Thread.Sleep(1000);

                foreach (var resp in sockets[i].response)
                {
                    resp.Split(',');
                }

                //For testing purpose. Fills response for sockets. Delete later.
                //sockets[i].response = RandomRSSIString().Split(',');
                for (int j = 0; j < sockets[i].response.Length - 1; j++)
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

                sockets[i].ShutdownClient();
            }

            GetMacAddress();

            for (int i = 0; i < dict["7c:7a:91:3b:1c:12"].Count(); i++)
            {
                meanlist[i] = (Double.Parse(dict["7c:7a:91:3b:1c:12"][i].TrimStart('-')));

            }

            dict.Clear();
        }

        public Reference CreateDataInstance()
        {
            Reference refItem = new Reference
            {
                rssI1 = Convert.ToInt32(meanlist[0]),
                rssI2 = Convert.ToInt32(meanlist[1]),
                rssI3 = Convert.ToInt32(meanlist[2])
            };

            return refItem;
        }


        public void ShutdownClient()
        {
            foreach (var sock in sockets)
            {
                sock.ShutdownClient();
            }
        }


        //Retrieves mac address for this device
        private void GetMacAddress()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                _mac = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes()).Replace('-', ':').ToLower();

                Console.WriteLine($"{_mac}");
                break;
            }
        }



    }
}
