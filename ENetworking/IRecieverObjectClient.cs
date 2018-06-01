using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetworking {
    /// <summary>
    /// Interface for standard data communication
    /// </summary>
    public interface IRecieverObjectClient {
        /// <summary>
        /// Initially ran in the beginning of launch
        /// </summary>
        void Run();

        /// <summary>
        /// Called when serialized data ready
        /// </summary>
        /// <param name="data">Deserialized Data</param>
        void RecieveData(object data);

        /// <summary>
        /// Serialization algorithm
        /// </summary>                       
        /// <param name="data">Object to be serialized</param>
        byte[] SerializeData(object data);

        /// <summary>
        /// Deserialization algorithm
        /// </summary>
        /// <typeparam name="T">Class type to be deserialized into</typeparam>
        /// <param name="vs">byte array for data</param>
        void DeserializeData<T>(byte[] vs);
    }
}
