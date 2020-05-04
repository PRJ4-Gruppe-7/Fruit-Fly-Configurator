using System;
using System.Collections.Generic;
using System.Text;

namespace FFC.Services.WebSocketService
{
    public interface IASyncSocket
    {
        //Connects to client
        void StartClient();

        //Sends request to client
        void Send(String data);

        //Receives response from client
        void Receive();

        //Shutdowns client
        void ShutdownClient();
    }
}
