using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace ENetworking {
    /// <summary>
    /// Uses ServerClient Object
    /// </summary>
    public class Server<C,T> where C : IServerClient<T> {


        public int port;
        private Socket ListenerSocket;
        public ISerializer _Serializer;
        private List<ClientConnectionArgs<T>> ClientConnections = new List<ENetworking.ClientConnectionArgs<T>>();
        public event RecieveHandler DataSerialized;





        public Server(int port) {
            this.port = port;
            _Serializer = new ENetworking.Serializer();
        }

        public Server(int port, ISerializer serializer) {
            this.port = port;
            _Serializer = serializer;
        }

        public void InitializeConnection() {


            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress iP = IPAddress.Any;
            IPEndPoint endPoint = new IPEndPoint(iP, port);
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenerSocket.Bind(endPoint);


        }


        public void Run(bool run) {
            try {
                while (run) {

                    Socket tempSocket = ListenerSocket.Accept();
                    int tempID = IDGenerator.GenerateID();
                    KeyValuePair<Socket, int> tempClientConnection = new KeyValuePair<Socket, int>(tempSocket, tempID);
                    ServerClient<T> serverClient = new ServerClient<T>(tempClientConnection, _Serializer, this);
                    Thread clientThread = new Thread(new ThreadStart(serverClient.Run));


                    ClientConnectionArgs<T> tempClientConnectionArgs = new ClientConnectionArgs<T>();
                    tempClientConnectionArgs.ID = tempID;
                    tempClientConnectionArgs.socket = tempSocket;
                    tempClientConnectionArgs.serverClient = serverClient;
                    tempClientConnectionArgs.thread = clientThread;

                    ClientConnections.Add(tempClientConnectionArgs);

                    clientThread.Start();

                    while (!clientThread.IsAlive) ;

                    serverClient.HostTransfer += DataSerialized;
                }
            }catch{}
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

