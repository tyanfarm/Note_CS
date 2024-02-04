namespace CS_Interface {
    class Interface {
        public interface IClassC {
            public void ActionC();
        }

        public class ClassC : IClassC {
            public ClassC() {
                Console.WriteLine("Class C created !");
            }

            public void ActionC() {
                Console.WriteLine("Action in class C");
            }
        }

        public class ClassC1 : IClassC {
            public ClassC1() {
                Console.WriteLine("Class C1 created !");
            }

            public void ActionC() {
                Console.WriteLine("Action in class C1");
            }
        }
    }
}