using CS_Delegate;
using CS_Event;
using CS_ExtendMethod;
using CS_Asynchronous;

namespace Note_CS {
    class Program {
        static async Task Main(string[] args) {
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

            // string s = "Con cac";
            // // ExtendMethod.Print(s, ConsoleColor.DarkBlue);
            // s.Print(ConsoleColor.DarkGreen);

            // int x = 5;
            // Console.WriteLine($"Cube number of {x} is {x.Cube()}");
            // Console.WriteLine($"Square number of {x} is {x.Square()}");

            //========================================
            
            // CS_Asynchronous
            Task t2 = Asynchronous.Task2();
            Task t3 = Asynchronous.Task3();
            Task<string> t5 = Asynchronous.Task5();

            Asynchronous.PrintContent(6, "Con cac 1", ConsoleColor.DarkYellow);

            await t2;
            await t3;
            await t5;

            Console.WriteLine("Press any thing");
        }
    }
}