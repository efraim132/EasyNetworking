using System;
using System.Collections.Generic;
namespace ENetworking {
    public class IDGenerator {
        private static List<int> history = new List<int>() { 0 };
        private static Random random = new Random(1234);

        public static int GenerateID {
            get {

                int output = 0;
                while (History.Contains(output)) {
                    output = random.Next();
                }
                return output;

            }
        }

        public static List<int> History { get => history; set => history = value; }
    }
}
