using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ENetworking.Serialization {
    class Serializer : ISerializer {
        public T DeSerialize<T>(byte[] vs) {
            MemoryStream ms = new MemoryStream(vs);
            BinaryFormatter bf = new BinaryFormatter();
            return (T) bf.Deserialize(ms);
        }

        public byte[] Serialize(object data) {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, data);
            return ms.ToArray();
        }
    }
}
