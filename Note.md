<div style="text-align: center; background-color: #b1d1ff; font-family: 'Trebuchet MS', Arial, sans-serif; color: white; padding: 5px; font-size: 30px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
Basic Syntax
</div>

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
