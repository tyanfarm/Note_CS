namespace CS_Delegate 
{
    public delegate void showLog(string message);

    class Program 
    {
        static void Info(string s) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        static void Warning(string s) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        // static void Main(string[] args) {
        //     // showLog? log = null;

        //     // log += Info;
        //     // log += Info;
        //     // log += Info;

        //     // log += Warning;
        //     // log += Warning;

        //     Action<string> log;
        //     log = Info;
        //     // log("Hello World");
        //     log?.Invoke("Con Cac");
        // }
    }

}

namespace CS_Event {
    class DataInput : EventArgs {
        public int data {get; set;}

        public DataInput(int x) {
            data = x;
        }
    }

    class UserInput {
        // ~ delegate void temp(object? sender, EventArgs args)
        public event EventHandler? eventCatcher;

        // publisher
        public void Input() {
            do {
                Console.Write("Enter number: ");
                string? s = Console.ReadLine();
                int i = Int32.Parse(s ?? string.Empty);

                eventCatcher?.Invoke(this, new DataInput(i));

            } while (true);
        }
    }

    class SquareNum {
        public void Subcriber(UserInput input) {
            input.eventCatcher += Calculate;
        }

        public void Calculate(object? sender, EventArgs e) {
            DataInput dataInput = (DataInput)e;
            int i = dataInput.data;

            Console.WriteLine($"Square Num of {i} is: {i * i}");
        }
    }

    class Program {
        static void Main(string[] args) {
            // publisher
            UserInput input = new UserInput();

            // subcriber
            SquareNum square = new SquareNum();
            square.Subcriber(input);

            input.Input();
        }
    }
}

