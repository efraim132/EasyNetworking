using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using ENetworking.Serialization;
using ENetworking;
using ENetworking.Client;

namespace ENetworking.Server {
    /// <summary>
    /// Uses ServerClient Object
    /// </summary>
    public class Server<C, T> where C : IServerClient<T>{


        public int port;
        private Socket ListenerSocket;
        public ISerializer _Serializer;
        private List<ClientConnectionArgs<T>> ClientConnections = new List<ClientConnectionArgs<T>>();
        public event RecieveHandler DataSerialized;







        public Server(int port) {
            this.port = port;
            _Serializer = new Serializer();
            InitializeConnection();



        }

        public Server(int port, ISerializer serializer) {
            this.port = port;
            _Serializer = serializer;
            InitializeConnection();
        }

        void InitializeConnection() {


            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress iP = IPAddress.Any;
            IPEndPoint endPoint = new IPEndPoint(iP, port);
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenerSocket.Bind(endPoint);
            ListenerSocket.Listen(10);


        }


        public void Run(bool run) {
            try {
                while (run) {
                    Console.WriteLine("Listening");
                    Socket tempSocket = ListenerSocket.Accept();
                    Console.WriteLine("accepted, creating child!");
                    int tempID = IDGenerator.GenerateID;
                    Console.WriteLine($"Id gen {tempID}");
                    KeyValuePair<Socket, int> tempClientConnection = new KeyValuePair<Socket, int>(tempSocket, tempID);
                    var serverClient = C;
                    Thread clientThread = new Thread(new ThreadStart(serverClient.Run));

                    clientThread.Start();

                    while (!clientThread.IsAlive) ;


                    serverClient.StartResponseListener();

                    ClientConnectionArgs<T> tempClientConnectionArgs = new ClientConnectionArgs<T>();
                    tempClientConnectionArgs.ID = tempID;
                    tempClientConnectionArgs.socket = tempSocket;
                    tempClientConnectionArgs.serverClient = serverClient;
                    tempClientConnectionArgs.thread = clientThread;

                    ClientConnections.Add(tempClientConnectionArgs);


                    
                    serverClient.HostTransfer += DataSerialized;
                }
                ShutDown();
            }catch(Exception e){
                Console.WriteLine(e);
                run = false;
            }
        }

        public void ShutDown(){
            foreach(ClientConnectionArgs<T> CCA in ClientConnections){
                CCA.serverClient.StopResponseListener();
                CCA.socket.Close();
                CCA.thread.Abort();
            }
        }



    }
}

