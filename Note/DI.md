<div style="text-align: center; background-color: palevioletred; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 36px; padding:24px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
Dependency Injection
</div>

# Dependency (phụ thuộc)
- Là khi 1 class này phụ thuộc và thường dùng để quản lý bởi 1 class khác

# Dependency Inversion
- Nguyên tắc: một module cấp cao không nên phụ thuộc vào module cấp thấp mà chúng chỉ lên phụ thuộc vào Interface.

- Sai:
    ```
    class LoginFaceBook
    {
        // Logic code
        function loginFace {

        }
    }

    class UserLogin
    {
        private $login;
        function __contruct(LoginFaceBook $logInFace) {
            $this->login = $loginFace;
        }

        // Logic code
    }
- Đúng:
    ```
    interface LoginWithAnOtherAccount
    {
        public function login();
    }   

    class LoginFaceBook implements LoginWithAnOtherAccount
    {
        // Code login login facebook
        public function login();
    }

    class UserLogin
    {
        private $login;
        function __contruct(LoginWithAnOtherAccount $login) {
            $this->login = $login;
        }
        
        // Logic tiếp theo ....
    }

# Inverse of Control (IOC) - Đảo ngược điều khiển
- Tham chiếu trực tiếp
![alt](https://raw.githubusercontent.com/xuanthulabnet/learn-cs-netcore/master/imgs/di-01.png)

- Tham chiếu đến interface
![alt](https://raw.githubusercontent.com/xuanthulabnet/learn-cs-netcore/master/imgs/di-02.png)

# Dependency Injection
- Có chức năng tiêm các `Dependency` vào và sử dụng

- Các module cấp module phụ thuộc sẽ được `inject` vào module cấp cao.
    + ![alt](https://blog.haposoft.com/content/images/2021/11/download-1.png)

- Khi khởi tạo 1 `attribute` trong class thì đây gọi là `dependency`

- Khi khởi tạo giá trị cho `attribute` bằng 1 đối tượng có kiểu dữ liệu giống nó từ ngoài vào thì đây gọi là `inject` 

- Các kiểu DI:
    + DI thông qua phương thức khởi tạo
        ```
        public class Horn {
            int level; // thêm độ lớn còi xe
            public Horn (int level) => this.level = level; // thêm khởi tạo level

            public void Beep () => Console.WriteLine ("Beep - beep - beep ...");
        }

        public class Car {
            // horn là một Dependecy của Car
            Horn horn;

            // dependency Horn được đưa vào Car qua hàm khởi tạo
            public Car(Horn _horn) => this.horn = _horn;

            public void Beep () {
                // Sử dụng Dependecy đã được Inject
                horn.Beep ();
            }
        }

        // Khi sử dụng
        Horn horn = new Horn(10);

        var car = new Car(horn); // horn inject vào car
        car.Beep(); // Beep - beep - beep ...
    + DI thông qua setter
    + DI thông qua Interface

# ServiceCollection
- Thư viện hỗ trợ DI của `C#`

- `IServiceCollection`: cho phép bạn đăng ký các dịch vụ (truy cập dữ liệu, logging, authentication, authorization, ...) trong quá trình khởi tạo ứng dụng.

- `IServiceProvider`:  cung cấp các phương thức để truy cập các dịch vụ được đăng ký trong `IServiceCollection` và chuyển chúng cho các thành phần khác trong ứng dụng.

- Cần sử dụng `BuildServiceProvider()` để tạo một `IServiceProvider`.

- Để sử dụng ta thực hiện các bước:
    + Cài đặt package:
        + `dotnet add package Microsoft.Extensions.DependencyInjection`

    + Khai báo
        + ```
            using Microsoft.Extensions.DependencyInjection;

            var services = new ServiceCollection();

            services.AddSingleton<Interface.IClassC, Interface.ClassC>();

            var providers = services.BuildServiceProvider();


### Singleton
- Giúp các đối tượng chỉ cần khởi tạo 1 lần
- `Không singleton`:
    + ```
        for (int i = 0; i < 5; i++) {
            Interface.IClassC c = new Interface.ClassC();
            
            Console.WriteLine(c?.GetHashCode());    
        }
    + Output Console:
        ```
        Class C created !
        43942917
        Class C created !
        59941933
        Class C created !
        2606490
        Class C created !
        23458411
        Class C created !
        9799115
    + Có thể thấy mỗi lần khởi tạo đều trả về 1 `Hash Code` khác nhau

- `Sử dụng Singleton`:
    + ```
        for (int i = 0; i < 5; i++) {
            Interface.IClassC? c = providers.GetService<Interface.IClassC>();

            Console.WriteLine(c?.GetHashCode());    
        }
    + Output Console:
        ```
        Class C created !
        43942917
        43942917
        43942917
        43942917
        43942917
    + `ClassC` chỉ khởi tạo đúng 1 lần

### Transient (Tạm thời)
- Mỗi lần sẽ khởi tạo 1 giá trị khác nhau

### Scoped
- Khởi tạo giá trị giống `Singleton` nhưng trong 1 phạm vi tự tạo riêng.

- ```
    for (int i = 0; i < 5; i++) {
        Interface.IClassC? c = providers.GetService<Interface.IClassC>();
        // Interface.IClassC c = new Interface.ClassC();

        Console.WriteLine(c?.GetHashCode()); 
    }

    using (var scope = providers.CreateScope()) {
        var provider1 = scope.ServiceProvider;

        for (int i = 0; i < 5; i++) {
            Interface.IClassC? c = provider1.GetService<Interface.IClassC>();
            // Interface.IClassC c = new Interface.ClassC();

            Console.WriteLine(c?.GetHashCode()); 
        }
    }
- Output console:
    ```
    Class C created !
    43942917
    43942917
    43942917
    43942917
    43942917
    Class C created !
    59941933
    59941933
    59941933
    59941933
    59941933
- Có thể thấy ở trên đoạn code ngoài `scope` và trong `scope` tạo ra 2 `Class C` khác nhau

### Delegate & Factory
- `readonly`: từ khóa để khai báo 1 `attribute` mà chỉ nhận được giá trị qua constructor.

- ```
    public interface IClassB {
            public void ActionB();
        }

    public class ClassB : IClassB {
        readonly IClassC _c;
        readonly string _msg;

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

- Ở đoạn code trên, `ClassB` có 1 `attribute` `dependency` `IClassC` nên khi khởi tạo cần thêm 2 tham số kèm theo.

- Nếu khởi tạo `Service` như cú pháp cũ là `services.AddSingleton<Interface.IClassB, Interface.ClassB>` thì lỗi vì khởi tạo `ClassB` yêu cầu 2 tham số. 
- Do đó ta sử dụng `delegate` như sau:
    + ```
        services.AddSingleton<Interface.IClassB, Interface.ClassB>(
            (providers) => {
                var b2 = new Interface.ClassB(
                    providers.GetService<Interface.IClassC>(),
                    "Processing in Class B !"
                );

                return b2;
            }
        );

- Có thể dùng phương pháp khác là khởi tạo kèm `Factory`

- `Factory` là 1 hàm riêng dùng để khởi tạo `service`
    + ```
        public static ClassB CreateB(IServiceProvider providers) {
            var b2 = new ClassB(
                providers.GetService<IClassC>(),
                "Processing in Class B !"
            );

            return b2;
        }
    + ```
        services.AddSingleton<Interface.IClassB>(Interface.CreateB);

### IOptions
- Khi một dịch vụ đăng ký trong DI, nếu nó cần các tham số để khởi tạo thì ta có thể Inject các tham số khởi tạo là các đối tượng như cách làm ở trên. Tuy nhiên để tách bạch giữa các dịch vụ và các thiết lập truyền vào để khởi tạo dịch vụ thì trong ServiceCollection hỗ trợ sử dụng giao diện IOptions.

- Add package: `dotnet add package Microsoft.Extensions.Options`

- Code:
    + ```
        public class MyServiceOptions {
            public string? data1 {get; set;}
            public int data2 {get; set;}
        }
    + ```
        public class MyService {
            public string? data1 {get; set;}
            public int data2 {get; set;}

            public MyService(IOptions<MyServiceOptions> options) {
                MyServiceOptions _options = options.Value;

                data1 = _options.data1;
                data2 = _options.data2;
            }

            public void PrintData() => Console.WriteLine($"{data1} / {data2}");

        }
    + Main:
    
    + ```
        services.Configure<MyServiceOptions>(
            (MyServiceOptions options) => {
                options.data1 = "Hello";
                options.data2 = 2;
            }
        );

        // Đăng ký dịch vụ MyService
        services.AddSingleton<MyService>();

        // Tạo IServiceProvider
        var providers = services.BuildServiceProvider();

        // Khởi tạo 1 MyService
        var myservice = providers.GetService<MyService>();

        myservice?.PrintData();
    + Console:
        ```
        Hello / 2
- Khi phương thức `MyService` được khởi tạo (`services.AddSingleton<MyService>();`) thì ở `constructor` của nó có 1 `IOptions` có kiểu `MyServiceOptions` nên nó sẽ tìm trong chương trình xem chỗ nào đăng ký `MyServiceOptions` (đăng ký qua `Configure<>`) thì nó sẽ lấy ra và `inject` vào `MyService` cho chúng ta khi `get` dịch vụ (`var myservice = providers.GetService<MyService>();`)

### Nạp cấu hình từ file
- Add package:
    + `dotnet add package Microsoft.Extensions.Configuration` 
    + `dotnet add package Microsoft.Extensions.Options.ConfigurationExtensions`
    + `dotnet add package Microsoft.Extensions.Configuration.Json`

- Using:
    ```
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;
- File json:
    ```
    {
        "section1": {
            "key1": "concac1",
            "key2": 50
        },

        "MyServiceOptions" : {
            "data1": "Fuck ur mum",
            "data2": 100
        }
    }
- Get value:
    ```
    // Biến lưu trữ cấu hình
    IConfigurationRoot configurationRoot;

    // Biến sử dụng để thiết lập cách ASP.NET Core sẽ đọc và load cấu hình
    ConfigurationBuilder configBuilder = new ConfigurationBuilder();

    // Thiết lập đường dẫn
    configBuilder.SetBasePath(Directory.GetCurrentDirectory());

    // Thêm tệp json
    configBuilder.AddJsonFile("configuration.json");

    configurationRoot = configBuilder.Build();

    // Từng section tương ứng với từng bậc trong file json
    var data1 = configurationRoot.GetSection("MyServiceOptions").GetSection("data1").Value;

    var data2 = configurationRoot.GetSection("MyServiceOptions").GetSection("data2").Value;

    Console.WriteLine($"{data1} in {data2}");
- Console:
    ```
    Fuck ur mum in 100
- Như vậy để đăng ký (`Configure<>`) ta sẽ thêm như sau:
    ```
    var jsonMyService = configurationRoot.GetSection("MyServiceOptions");

    services.Configure<MyServiceOptions>(jsonMyService);
