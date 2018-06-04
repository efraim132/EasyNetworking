using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ENetworking.Serialization;
namespace ENetworking {
    public class ServerClient<T> : IServerClient<T>{
        private KeyValuePair<Socket, int> ClientData;
        Thread ListenerThread;
        Listener _Listener;
        ISerializer seriealizer;
        Server<T> host;
        public event RecieveHandler HostTransfer;



        public ServerClient(KeyValuePair<Socket, int> keyValuePair, ISerializer serializer, Server<T> host) {
            Console.WriteLine("CLientCreated!");
            ClientData = keyValuePair;
            this.seriealizer = serializer;
            this.host = host;
        }

        public ServerClient(KeyValuePair<Socket, int> keyValuePair, Server<T> host) {
            ClientData = keyValuePair;
            this.seriealizer = new Serializer();
            this.host = host;
            Console.WriteLine("CLientCreated!");
        }





        public void Run() {
            throw new NotImplementedException();
        }

        public Socket GetSocket() { return ClientData.Key; }

        public void DataIncoming(byte[] vs) => HostTransfer.Invoke(seriealizer.DeSerialize<T>(vs), ClientData.Value);

        public void StartResponseListener() {
            _Listener = new Listener(this);
            ListenerThread = new Thread(new ThreadStart(_Listener.run));
            ListenerThread.Start();
            while (!ListenerThread.IsAlive) ;
        }

        public void StopResponseListener() => ListenerThread.Abort();
    }
}
