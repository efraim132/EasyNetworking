using System;
using System.Collections.Generic;
namespace ENetworking {
    public class IDGenerator {
        public static List<int> History = new List<int>();
        private static Random random = new Random(1234);
        public static int GenerateID(){

            int output = Int16.MaxValue;
            while(!History.Contains(output)){
                output = random.Next();
            }
            return output;

        }
    }
}
