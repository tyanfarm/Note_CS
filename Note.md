<div style="text-align: center; background-color: salmon; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 40px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
Basic Syntax
</div>

<br/>

# Static (thuộc tính tĩnh)
- ```
    public class MyClass
    {
        // Phương thức tĩnh có thể được gọi từ mọi nơi.
        public static void MyStaticMethod()
        {
            // ...
        }
    }
- Ví dụ trên cho thấy `MyStaticMethod` có thể được gọi trực tiếp từ tên của lớp (`MyClass.MyStaticMethod()`) mà không cần tạo một thể hiện của lớp.

- Nếu không có `static`, bạn sẽ cần tạo một thể hiện của lớp trước khi gọi `method` 
    + ```
        MyClass instance = new MyClass(); 
        instance.MyNonStaticMethod();
<br/>

# Namespace
- Trong namespace sẽ có `class`, `struct`, `enum`, `interface`, ..., `namespace`


- Ở các file khác ta import namespace bằng `using`

- `using static System.Console`: ta có thể dùng lệnh `WriteLine()` mà không cần `Console` ở trước

- `partial`: nếu ta có 1 `class` hoặc 1 gì khác cần khai báo trên nhiều file thì ta sẽ dùng `partial`
    + File 1:

        ```
            namespace ProductManage {
                public partial class Product {
                    public string name; 
                }
            }
    + File 2:
        ```
            namespace ProductManage {
                public partial class Product {
                    public float price; 
                }
            }
    + Class `Product` sẽ có 2 attribute là `name` và `price`

<br/>
<br/>

# Delegate (Con trỏ hàm)
- Cú pháp
    + `public delegate void showLog(string message);`

- Ví dụ
     ```
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

                static void Main(string[] args) {
                    showLog log = Info;

                    log("Hello World");
                }
            }
        }

- Hàm trên để in ra chữ với các màu khác nhau

- Cú pháp `Invoke` tương tự với gọi thẳng `log`
    + `log?.Invoke("Hello World");`

- Ở hàm `Main` có thể cho con trỏ hàm mang nhiều giá trị hàm
    ```

        static void Main(string[] args) {
            showLog? log = null;

            log += Info;
            log += Info;
            log += Info;

            log?.Invoke("Con Cac");
        }

- Khi này hàm sẽ in ra 3 dòng `Con Cac` với màu là `Green`

> **Action & Func**
- **Action**
    + Không có kiểu dữ liệu trả về (`void`)
    + ```
        // ~ delegate void log(string s);
        Action<string> log;  

        // ~ delegate void temp(int a, float b);
        Action<int, float> temp;
- **Func**
    + Tham số cuối cùng trong khai báo Func là kiểu trả về của hàm, có thể thiếu tham số nhưng không được thiếu kiểu trả về

    + ```
        // ~ delegate int calculate(int a, int b);
        Func<int, int, int> calculate;  

        // ~ delegate string temp(string a, int b);
        Func<string, int, string> temp;

# Event Handler
- `publisher`: class phát đi sự kiện

- `subcriber`: class nhận sự kiện

- Cú pháp:
    + ```
        // ~ delegate void temp(object? sender, EventArgs args)
        public event EventHandler? eventCatcher;
    + `sender`: class nào gửi sự kiện đi
    + `EventArgs`: mang dữ liệu mà sự kiện gửi đi

- Để thực hiện với `EventArgs` thì ta cần tạo ra 1 class kế thừa:
    ```
    class DataInput : EventArgs {
        public int data {get; set;}

        public DataInput(int x) {
            data = x;
        }
    }
- `publisher`:
    ```
    eventCatcher?.Invoke(this, new DataInput(i));
- `subcriber`:
    ```
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

- Code mẫu:
    ```
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

# Extension Method (Phương pháp mở rộng)
- Lưu ý: `method` phải là `static` và nằm trong `static class`
- Sử dụng method bình thường: 
    + ```
        static class ExtendMethod {
            public static void Print(string s, ConsoleColor color) {
                Console.ForegroundColor = color;
                Console.WriteLine(s);
                Console.ResetColor();
            }
        }

    + ```
        static void Main(string[] args) {
            string s = "Con cac";
            ExtendMethod.Print(s, ConsoleColor.DarkBlue);
        }

- Khi dùng `extend method` ta sẽ sử dụng `this`
    + ```
        static class ExtendMethod {
            public static void Print(this string s, ConsoleColor color) {
                Console.ForegroundColor = color;
                Console.WriteLine(s);
                Console.ResetColor();
            }
        }
    + ```
        static void Main(string[] args) {
            string s = "Con cac";
            s.Print(ConsoleColor.DarkGreen);
        }
    + Có thể thấy ở trên là khi đặt `this` ở trước `string s` trong hàm `Print()` thì ta có thể gọi hàm thẳng qua biến `string`

- Ví dụ thêm:
    + ```
        static class ExtendMethod {
            public static double Square(this int x) {
                return x * x;
            }

            public static double Cube(this int x) {
                return x * x * x;
            }
        }
    + ```
        static void Main(string[] args) {
            int x = 5;

            Console.WriteLine($"Cube number of {x} is {x.Cube()}");
            Console.WriteLine($"Square number of {x} is {x.Square()}");
        }

# Asynchronous (Bất đồng bộ)

### Multi Thread (Đa luồng)
- Để làm việc với các tiến trình ta sẽ sử dụng `Task`

- `Task` thực chất là 1 object mang giá trị 1 con trỏ hàm (`delegate`) trỏ tới các `Action`

- Có 2 loại:
    + `() => {}`

        ```
        Task t2 = new Task(
            () => {
                Asynchronous.PrintContent(10, "Con cac 2", ConsoleColor.DarkBlue);
            }
        );
    + `(object obj) => {}`

        ```
        Task t3 = new Task(
            (object? obj) => {
                string taskName = (string?)obj ?? "NULL";
                Asynchronous.PrintContent(10, taskName, ConsoleColor.DarkGreen);
            }
        , "Con cac 3");
- Khi chạy hàm dưới đây thì thứ tự xuất hiện sẽ lộn xộn

    + ```
        class Asynchronous {
            public static void PrintContent(int seconds, string msg, ConsoleColor color) {
                Console.ForegroundColor = color;
                Console.WriteLine($"{msg, 10} ... Start");
                Console.ResetColor();

                for (int i = 0; i < seconds; i++) {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{msg, 10} {i, 3}");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }

                Console.ForegroundColor = color;
                Console.WriteLine($"{msg, 10} ... End");
                Console.ResetColor();
            }
        }
    + ```
        Task t2 = new Task(
            () => {
                Asynchronous.PrintContent(10, "Con cac 2", ConsoleColor.DarkBlue);
            }
        );

        Task t3 = new Task(
            (object? obj) => {
                string taskName = (string?)obj ?? "NULL";
                Asynchronous.PrintContent(10, taskName, ConsoleColor.DarkGreen);
            }
        , "Con cac 3");

        t2.Start(); // Thread
        t3.Start(); // Thread

        Asynchronous.PrintContent(10, "Con cac 1", ConsoleColor.DarkYellow);
    + Màn hình console:

        + ![alt](https://i.pinimg.com/originals/38/41/90/38419057904508726058449d8d4ae99a.jpg)

- Có thể thấy khi in ra các `Con cac` in các màu lẫn lộn do các thread tranh giành trong hàm `PrintContent`

- Do đó ta sẽ thêm `lock(Console.Out)` để các `thread` chay đồng đều và không tranh giành tài nguyên lẫn nhau 
    ```
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

- Màn hình console:
    + ![alt](https://i.pinimg.com/736x/0d/9e/d4/0d9ed4d91b5dae1fb1bfabe55e6a2301.jpg)   

- Tuy nhiên khi `Con cac 1` kết thúc (vì nó là `thread` chính) thì nó kết thúc hết các `thread` khác (ở đây `Con cac 2` chưa kết thúc) 

- Để khắc phục ta sẽ dùng `Wait()` 
    + `Wait` ở đây có nghĩa là đợi tác vụ đó thực hiện xong

    + Có 2 cách:
        + ```
            t2.Wait();
            t3.Wait();
        + ```
            Task.WaitAll(t2, t3);

### Async / Await
- Khi chương trình đọc hàm `async` thì chương trình chạy tiếp các câu lệnh bên dưới. Đến khi gặp từ khóa `await` thì chương trình sẽ đợi tác vụ `async` bên trên hoàn thành (không chặn luồng thực thi)

- Các công việc sau `await` chỉ được thực hiện khi `Task` `async` hoàn thành.

- `Await` khác `Wait()` ở chỗ:

    + `Wait()` sẽ chặn các tác vụ khác hoạt động đến khi nào tác vụ nó đang đợi hoàn thành.

        + ```
            public static Task Task2() {
                Task t2 = new Task(
                    () => {
                        Asynchronous.PrintContent(10, "Con cac 2", ConsoleColor.DarkBlue);
                    }
                );

                t2.Start();
                t2.Wait();

                Console.WriteLine("T2 finish");
                
                return t2;
            }

            public static Task Task3() {
                Task t3 = new Task(
                    (object? obj) => {
                        string taskName = (string?)obj ?? "NULL";
                        Asynchronous.PrintContent(4, taskName, ConsoleColor.DarkGreen);
                    }
                , "Con cac 3");

                t3.Start();
                t3.Wait();

                Console.WriteLine("T3 finish");

                return t3;
            }
        + ```
            Task t2 = Asynchronous.Task2();
            Task t3 = Asynchronous.Task3();

            Asynchronous.PrintContent(6, "Con cac 1", ConsoleColor.DarkYellow);

            Console.WriteLine("Press any thing");
        + `t2` sẽ chạy xong sau khi in `T2 finish` thì `t3` mới bắt đầu chạy, sau đó là `Con cac 1`

        + Màn hình console:

        + ![alt](https://i.pinimg.com/736x/da/bb/6f/dabb6f797d648d704e379c0df580ea89.jpg)

    + `Await` sẽ đợi tác vụ của nó hoàn thành đồng thời không chặn các tác vụ khác chạy.
    
    + Một hàm `await` thì cần phải có ghi `async` ở trên khai báo hàm.
        
        + `await t2` sẽ đợi tác vụ `t2` hoàn thành xong mới chạy câu lệnh `Console.WriteLine("T2 finish");`

            ```
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
        + ===============================================
        + Ở `main`, nếu không `await t2` & `await t3` thì sau khi chạy xong tác vụ `Con cac 1` nó sẽ in `Press any thing` sau đó kết thúc chương trình, không quan tâm `t2` / `t3` đã chạy xong chưa `=>` Sử dụng `await`

            ```
            static async Task Main(string[] args) {
                Task t2 = Asynchronous.Task2();
                Task t3 = Asynchronous.Task3();

                Asynchronous.PrintContent(6, "Con cac 1", ConsoleColor.DarkYellow);

                await t2;
                await t3;

                Console.WriteLine("Press any thing");
            }
- Hàm `Task` có giá trị trả về:
    + Không có `await`:

        ```
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

    + Có `await`:

        ```
        public static async Task<string> Task5() {
            Task<string> t5 = new Task<string>(
                () => {
                    Asynchronous.PrintContent(3, "Con cac 5", ConsoleColor.Black);

                    return $"Return from Con cac 5";
                }
            );

            t5.Start();
            await t5;

            Console.WriteLine("T2 finish");

            return t5.Result;
        }
- Với `Task5()` có giá trị trả về là `string`, ta có thể in chuỗi kết quả này bằng cú pháp `t5.Result()`

    