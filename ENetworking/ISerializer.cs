using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetworking {
    public interface ISerializer {
        T DeSerialize<T>(byte[] vs);
        byte[] Serialize(object data);
    }
}
