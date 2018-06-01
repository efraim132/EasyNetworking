using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ENetworking {
    /// <summary>
    /// Server client, T is dataType
    /// </summary>
    public interface IServerClient<T> : IListener{
        /// <summary>
        /// Run this instance.
        /// </summary>
        void Run();
    }
}
