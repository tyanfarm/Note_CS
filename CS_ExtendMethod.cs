namespace CS_ExtendMethod {
    static class ExtendMethod {
        public static void Print(this string s, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        public static double Square(this int x) {
            return x * x;
        }

        public static double Cube(this int x) {
            return x * x * x;
        }
    }
}