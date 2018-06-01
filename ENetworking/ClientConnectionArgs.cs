using System;
using System.Net.Sockets;
using System.Threading;

namespace ENetworking {
    public struct ClientConnectionArgs<T> {
        public Socket socket;
        public int ID;
        public Thread thread;
        public IServerClient<T> serverClient;
    }
}
