using FFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFC.Services.WebSocketService
{
    public interface IWebsocketService
    {
        void InitiateClient();
        void ReceiveResponse();
        Reference CreateInstance();
    }
}
