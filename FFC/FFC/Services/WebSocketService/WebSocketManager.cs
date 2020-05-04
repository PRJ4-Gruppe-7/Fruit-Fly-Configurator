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

        //Check interface for websocketservice for elaboration of functionality
        public void InitiateClient()
        {
            websocketService.InitiateClient();
        }

        //Check interface for websocketservice for elaboration of functionality
        public void ReceiveAndProcessResponse()
        {
            websocketService.ReceiveResponse();
        }

        //Check interface for websocketservice for elaboration of functionality
        public Reference CreateDataInstance()
        {
            return websocketService.CreateDataInstance();
        }
    }
}
