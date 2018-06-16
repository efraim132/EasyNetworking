using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetworking {
    interface ClientCollection<T> {
        void Add(IServerClient<T> Client);
        IServerClient<T> Get();
        
    }
}
