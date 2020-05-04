using FFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFC.Services.WebSocketService
{
    public interface IWebsocketService
    {
        //Sets up all sockets for each sniffer and connects to each of them
        void InitiateClient();

        //Receives response from each client, processes the data and stores data
        //in respective containers
        void ReceiveResponse();

        //Creates instance of Reference and assigns mean RSSI values from each sniffer
        //and returns the instance
        Reference CreateDataInstance();
    }
}
