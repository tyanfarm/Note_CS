namespace CS_Delegate 
{
    public delegate void showLog(string message);

    class DelegateFunc
    {
        public static void Info(string s) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        public static void Warning(string s) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(s);
            Console.ResetColor();
        }
    }

}