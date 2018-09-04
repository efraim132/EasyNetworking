using System;
using System.Collections.Generic;
namespace ENetworking.Server {
    public class ClientCollection<C, T> : IClientCollection<C, T> where C : IServerClient<T> {
        
        public void Add(IServerClient<T> Client) {
            throw new NotImplementedException();
        }

        public IServerClient<T> Get() {
            throw new NotImplementedException();
        }
    }
}
