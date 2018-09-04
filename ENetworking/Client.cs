using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ENetworking.Serialization;

namespace ENetworking.Client
{
    /// <summary>
    /// Recieving Handler
    /// </summary>
    /// <param name="obj">object data</param>
    /// <param name="id">client associated with object</param>
    public delegate void RecieveHandler(object obj, int id);

    public class Client<T> : IListener {
        
        public event RecieveHandler DataSerialized;
        private string hostname;
        private int port;
        private Socket ServerListenerSocket;
        private Thread ServerListenerThread;
        private ISerializer Serializer;
        


        public Client(string hostname,int port) {
            this.hostname = hostname;
            this.port = port;
            Serializer = new Serializer();
        }

        public Client(string hostname, int port, ISerializer serializer) {
            this.hostname = hostname;
            this.port = port;
            Serializer = serializer;
        }

        /// <summary>
        /// Creates server listenersocket
        /// </summary>
        /// <param name="networkAdapter">Computer network adapter index</param>
        public void InitializeConnection(int networkAdapter) {
            IPHostEntry iP = Dns.GetHostEntry(hostname);
            IPAddress iPAddress = iP.AddressList[networkAdapter];
            IPEndPoint iPEnd = new IPEndPoint(iPAddress, port);
            ServerListenerSocket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Creates server listenersocket
        /// </summary>
        /// <param name="socketInformation"></param>
        public void InitializeConnection(SocketInformation socketInformation) {
            ServerListenerSocket = new Socket(socketInformation);
        }

        public void DataIncoming(byte[] vs) => DataSerialized.Invoke(Serializer.DeSerialize<T>(vs), 0);

        public Socket GetSocket() => ServerListenerSocket;



        public void StartResponseListener() {
            Listener Listener = new Listener(this);
            ServerListenerThread = new Thread(new ThreadStart(Listener.run));
            while (!ServerListenerThread.IsAlive) ;
        }

        public void StopResponseListener() => ServerListenerThread.Abort();

        public void Send(T data) => ServerListenerSocket.Send(
                                         Serializer.Serialize(data));


        
    }
}
