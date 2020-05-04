using FFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFC.Services.WebSocketService
{



    public class WebSocketManager
    {
        IWebsocketService websocketService;

        public WebSocketManager(IWebsocketService service)
        {
            websocketService = service;
        }

        public void InitiateClient()
        {
            websocketService.InitiateClient();
        }

        public void ReceiveAndProcessResponse()
        {
            websocketService.ReceiveResponse();
        }

        public Reference CreateDataInstance()
        {
            return websocketService.CreateDataInstance();
        }
    }
}
