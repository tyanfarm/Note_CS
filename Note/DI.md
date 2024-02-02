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
