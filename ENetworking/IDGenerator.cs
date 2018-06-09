using System;
using System.Collections.Generic;
namespace ENetworking {
    public class IDGenerator {
        public static List<int> History = new List<int>(){0};
        private static Random random = new Random(1234);

        public static int GenerateID(){

            int output = 0;
            while(History.Contains(output)){
                output = random.Next();
            }
            return output;

        }
    }
}
