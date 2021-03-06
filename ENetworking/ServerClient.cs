﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ENetworking.Serialization;
using ENetworking.Client;
namespace ENetworking.Server {
    public class ServerClient<C, T> : IServerClient<T>{
        private KeyValuePair<Socket, int> ClientData;
        Thread ListenerThread;
        Listener _Listener;
        ISerializer serializer;
        Server<C,T> host;
        public event RecieveHandler HostTransfer;



        public ServerClient(KeyValuePair<Socket, int> keyValuePair, ISerializer serializer, Server<C,T> host) {
            Console.WriteLine("ClientCreated!");
            ClientData = keyValuePair;
            this.serializer = serializer;
            this.host = host;
        }

        public ServerClient(KeyValuePair<Socket, int> keyValuePair, Server<C,T> host) {
            ClientData = keyValuePair;
            serializer = new Serializer();
            this.host = host;
            Console.WriteLine("ClientCreated!");
        }





        public void Run() {
            Console.WriteLine($"I, child:{ClientData.Value} have been born!");
        }

        public Socket GetSocket() { return ClientData.Key; }

        public void DataIncoming(byte[] vs) => HostTransfer.Invoke(serializer.DeSerialize<T>(vs), ClientData.Value);

        public void StartResponseListener() {
            _Listener = new Listener(this);
            ListenerThread = new Thread(new ThreadStart(_Listener.run));
            ListenerThread.Start();
            while (!ListenerThread.IsAlive) ;
        }

        public void StopResponseListener() => ListenerThread.Abort();
    }
}
