using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using FFC.Services.WebSocketService;

/*
 * https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-client-socket-example
 */
namespace FFC.Services
{
    public struct SnifferSource
    {
        public string name;
        public string hostname;
        public string numericHostName;
        public int port;
    }

    public class StateObject
        {
            public Socket workSocket = null;
            public const int ReceiveBufferSize = 256;
            public byte[] Receivebuffer = new byte[ReceiveBufferSize];
            public StringBuilder sb = new StringBuilder();
        }
        public class ASyncSocket : IASyncSocket
        {
            private int port;
            private string ip;
            

            private static ManualResetEvent connectDone = new ManualResetEvent(false);
            private static ManualResetEvent sendDone = new ManualResetEvent(false);
            private static ManualResetEvent receiveDone = new ManualResetEvent(false);

            private IPAddress ipAddress;
            private IPEndPoint ipEndPoint;
            private Socket client;
            private StateObject state = new StateObject();

            public string[] response;

            public ASyncSocket(string ip_, int port_)
            {
                port = port_;
                ip = ip_;

            }

            public void StartClient()
            {
                try
                {
                    ipAddress = IPAddress.Parse(ip);
                    ipEndPoint = new IPEndPoint(ipAddress, port);

                    client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    client.BeginConnect(ipEndPoint, new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }


            private void ConnectCallback(IAsyncResult ar)
            {
                try
                {
                    Socket client = (Socket) ar.AsyncState;
                    client.EndConnect(ar);
                    connectDone.Set();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.ToString()); 

                }
            }

            public void Receive()
            {
                try
                {
                    if (state.workSocket == null)
                        state.workSocket = client;

                    client.BeginReceive(state.Receivebuffer, 0, StateObject.ReceiveBufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    receiveDone.WaitOne();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            private void ReceiveCallback(IAsyncResult ar)
            {
                try
                {
                    StateObject state = (StateObject) ar.AsyncState;
                    Socket client = state.workSocket;

                    int bytesRead = client.EndReceive(ar);

                    if (bytesRead > 0)
                    {
                        state.sb.Append(Encoding.ASCII.GetString(state.Receivebuffer, 0, bytesRead));

                        if (state.sb.Length > 1)
                        {
                            response = state.sb.ToString().Split(',');
                        }

                        receiveDone.Set();
                        state.sb.Clear();
                        //client.BeginReceive(state.Receivebuffer, 0, StateObject.ReceiveBufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    }
                    else
                    {
                            if (state.sb.Length > 1)
                            {
                                //response = state.sb.ToString();
                            }

                            receiveDone.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            public void Send(String data)
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
                sendDone.WaitOne();
            }

            private void SendCallback(IAsyncResult ar)
            {
                try
                {
                    Socket client = (Socket) ar.AsyncState;

                    int bytesSent = client.EndSend(ar);
                    //Console.WriteLine("Sent {0} bytes to server.", bytesSent);
 
                    sendDone.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            public void ShutdownClient()
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
        }
        }
    }

