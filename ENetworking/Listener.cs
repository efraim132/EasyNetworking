using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ENetworking {
    class Listener {
        public IListener Host;
        private byte[] vs = new byte[1024];

        public Listener(IListener host) { Host = host; }

        public void run() {
            Socket ServerListener = Host.GetSocket();
            while (true) {
                ServerListener.Receive(vs);
                Host.DataIncoming(vs);
                vs = new byte[1024];
            }
        }
    }
}
