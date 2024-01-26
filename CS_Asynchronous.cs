namespace CS_Asynchronous {
    class Asynchronous {
        public static async Task Task2() {
            Task t2 = new Task(
                () => {
                    Asynchronous.PrintContent(10, "Con cac 2", ConsoleColor.DarkBlue);
                }
            );

            t2.Start();
            await t2;

            Console.WriteLine("T2 finish");
        }

        public static async Task Task3() {
            Task t3 = new Task( 
                (object? obj) => {
                    string taskName = (string?)obj ?? "NULL";
                    Asynchronous.PrintContent(4, taskName, ConsoleColor.DarkGreen);
                }
            , "Con cac 3");

            t3.Start();
            await t3;

            Console.WriteLine("T3 finish");
        }

        public static Task<string> Task4() {
            Task<string> t4 = new Task<string>( 
                (object? obj) => {
                    string taskName = (string?)obj ?? "NULL";
                    Asynchronous.PrintContent(5, taskName, ConsoleColor.DarkRed);

                    return $"Return from {taskName}";
                }
            , "Con cac 4");

            t4.Start();

            Console.WriteLine("T4 finish");

            return t4;
        }

        public static async Task<string> Task5() {
            Task<string> t5 = new Task<string>(
                () => {
                    Asynchronous.PrintContent(3, "Con cac 5", ConsoleColor.Black);

                    return $"Return from Con cac 5";
                }
            );

            t5.Start();
            await t5;

            Console.WriteLine("T5 finish");

            return t5.Result;
        }

        public static void PrintContent(int seconds, string msg, ConsoleColor color) {
            lock(Console.Out) {
                Console.ForegroundColor = color;
                Console.WriteLine($"{msg, 10} ... Start");
                Console.ResetColor();
            }

            for (int i = 0; i < seconds; i++) {
                lock(Console.Out) {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{msg, 10} {i, 3}");
                    Console.ResetColor();
                }
                
                    Thread.Sleep(1000);
            }

            lock(Console.Out) {
                Console.ForegroundColor = color;
                Console.WriteLine($"{msg, 10} ... End");
                Console.ResetColor();
            }
        }
    }
}