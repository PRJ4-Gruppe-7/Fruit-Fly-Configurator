using System;
using System.Collections.Generic;
using System.Text;

namespace FFC.Services.WebSocketService
{
    public interface IASyncSocket
    {
        void StartClient();
        void Receive();
        void Send(String data);
        void ShutdownClient();
    }
}
