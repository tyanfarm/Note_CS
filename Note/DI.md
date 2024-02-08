<div style="text-align: center; background-color: palevioletred; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 36px; padding:24px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
Dependency Injection
</div>

# Dependency (phụ thuộc)
- Là khi 1 class này phụ thuộc và thường dùng để quản lý bởi 1 class khác

# Inverse of Control (IOC) - Đảo ngược điều khiển
- Tham chiếu trực tiếp
![alt](https://raw.githubusercontent.com/xuanthulabnet/learn-cs-netcore/master/imgs/di-01.png)

- Tham chiếu đến interface
![alt](https://raw.githubusercontent.com/xuanthulabnet/learn-cs-netcore/master/imgs/di-02.png)

# Dependency Injection
- Có chức năng tiêm các `Dependency` vào và sử dụng

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
- ```
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

