using CS_Delegate;
using CS_Event;
using CS_ExtendMethod;

namespace Note_CS {
    class Program {
        static void Main(string[] args) {
            // // CS_Delegate

            // Action<string>? log = null;
            // log += DelegateFunc.Info;
            // log += DelegateFunc.Warning;
            // log += DelegateFunc.Info;

            // log?.Invoke("Con Cac");

            //========================================

            // // CS_Event

            // // publisher
            // UserInput input = new UserInput();

            // // subcriber
            // SquareNum square = new SquareNum();
            // square.Subcriber(input);

            // input.Input();

            //========================================

            // CS_ExtendMethod

            string s = "Con cac";
            // ExtendMethod.Print(s, ConsoleColor.DarkBlue);
            s.Print(ConsoleColor.DarkGreen);

            int x = 5;
            Console.WriteLine($"Cube number of {x} is {x.Cube()}");
            Console.WriteLine($"Square number of {x} is {x.Square()}");
        }
    }
}