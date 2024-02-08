using Microsoft.Extensions.DependencyInjection;

namespace CS_Interface {
    class Interface {
        public interface IClassB {
            public void ActionB();
        }

        public class ClassB : IClassB {
            IClassC _c;
            string _msg;

            public ClassB(IClassC c, string msg) {
                _c = c;
                _msg = msg;
                Console.WriteLine("ClassB2 is created !");
            }

            public void ActionB() {
                Console.WriteLine(_msg);
                _c.ActionC();
            }
        }

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

        public static IClassB CreateB(IServiceProvider providers) {
            var b2 = new ClassB(
                providers.GetService<IClassC>(),
                "Processing in Class B !"
            );

            return b2;
        }
    }
}