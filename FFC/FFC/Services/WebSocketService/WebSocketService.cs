using System;
using System.Collections.Generic;
using System.Text;

namespace FFC.Services.WebSocketService
{
    public class WebSocketService
    {
        public void ReceiveRssiValuesFromSniffers()
        {
            //for (int i = 0; i < Sniffer_Count; i++)
            //{
            //    ASyncSocket s = new ASyncSocket(sources[0].numericHostName, sources[0].port);
            //    sockets[i] = s;
            //    //s.StartClient();
            //}

            //for (int i = 0; i < Sniffer_Count; i++)
            //{
            //    //sockets[i].Send("RETR test.txt");
            //    //sockets[i].Receive();

            //    //For testing purpose. Fills response for sockets.
            //    sockets[i].response = RandomRSSIString().Split(',');

            //    for (int j = 0; j < sockets[i].response.Length; j++)
            //    {
            //        var thisItem = sockets[i].response[j].Split(':');
            //        try
            //        {
            //            // Try adding the ip as a key, into the dictionary.
            //            // If successful, create the list containing the received signal strength values
            //            dict.Add(thisItem[0], new List<string>() { thisItem[1] });
            //        }
            //        catch (ArgumentException)
            //        {
            //            // If the key already exist in the dictionary, we add the new rssi value
            //            // to the list linked to that key.
            //            dict[thisItem[0]].Add(thisItem[1]);
            //        }
            //    }
            //}


            //// Make the list of RSSI values.
            //foreach (KeyValuePair<string, List<string>> p in dict)
            //{
            //    RSSIList.Add(p.Value.Select(int.Parse).ToList());
            //}

            //foreach (var item in RSSIList)
            //{
            //    Console.WriteLine("Item {0}", item);
            //    foreach (var i in item)
            //    {
            //        Console.Write("{0},", i);
            //    }
            //    Console.WriteLine("");
            //}

            //RSSIList.Clear();
            //dict.Clear();
        }
    }
}
