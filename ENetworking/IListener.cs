using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ENetworking {
    public interface IListener {
        Socket GetSocket();
        void DataIncoming(byte[] vs);
        void StartResponseListener();
        void StopResponseListener();

    }
}
