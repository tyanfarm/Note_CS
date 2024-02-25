<div style="text-align: center; background-color: #FFBFBF; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 40px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
ADO.NET
</div>

# ADO.NET là gì
- ADO.Net là 1 Framework làm việc với CSDL trong các ứng dụng

- Cho phép kết nối và tương tác với các loại cơ sở dữ liệu khác nhau như SQL Server, MySQL, Oracle, SQLite, và nhiều hơn nữa

- Cơ bản chia ra 2 khu vực như hình dưới:
    + ![alt](https://raw.githubusercontent.com/xuanthulabnet/learn-cs-netcore/master/imgs/cs058.png)

# Install package
- Lên web `https://www.nuget.org/packages/MySql.Data/` để lấy lệnh cài đặt package mới nhất về

# Docker for MySQL
- Khi dùng docker nó sẽ tạo 1 môi trường ảo trong 1 `container` rồi cài `MySQL` trên `container` đó. Sau đó `docker` sẽ map 1:1 với cái `port` từ container ra máy mình.

- Thông qua cái cổng chính nó sẽ nhảy vào cổng ảo, từ cổng ảo nó nhảy vào cái môi trường ảo nơi chứa cái `MySQL`.

# Tạo file docker yml
- `docker-compose.yml`: 
    + ```
        version: "3"

        services:
            netcore-mysql:
            image: "mysql:5.7"
            container_name: mysql-net
            restart: always
            hostname: mysqlnet
            networks:
                - my-network
            environment:
                MYSQL_ROOT_PASSWORD: abc123   #Thiết lập password cho root
            volumes:
                - storedb:/var/lib/mysql
            ports:
                - "3307:3306"                # Ánh xạ cổng 3307 vào cổng mặc định 3306 SQL

        networks:                            # TẠO NETWORK
        my-network:
            driver: bridge

        volumes:                              # TẠO Ổ ĐĨA LƯU DB
        storedb:

        # localhost 127.0.0.1 - port 3307 - root abc123 
<br/>

# Test Connection với MySQL Workbench
- ![alt](https://i.pinimg.com/736x/35/6a/08/356a0862876e1ebff5b23fc0820973a6.jpg)

- Ấn vô `Store in Vault` để nhập `password`

- `Connection successfully`:

    + ![alt](https://i.pinimg.com/736x/58/13/c8/5813c869bb296c4bfe3d577ce4b44edf.jpg)

# Tạo Connection với MySQl bằng SqlConnection()
- ```
    using System.Data.Common;
    using MySql.Data.MySqlClient;

    var sqlStringBuilder = new MySqlConnectionStringBuilder
    {
        ["Server"] = "localhost",
        ["Database"] = "tyanlab",

        // UserID
        ["UID"] = "root",
        ["PWD"] = "abc123",

        // Port ảo của MySQL trong Docker
        ["Port"] = "3307"
    };

    var sqlStringConnection = sqlStringBuilder.ToString();
    Console.WriteLine(sqlStringConnection);

    // using giải phóng `MySqlConnection`
    using var connection = new MySqlConnection(sqlStringConnection);

    connection.Open();
    
    connection.Close();
<br/>

# SqlCommand()
- `Chọn 10 sản phẩm đầu tiên`

    + ```
        // QUERY
        using var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Sanpham Limit 0, 10";

        var dataReader = command.ExecuteReader();

        while(dataReader.Read()) {
            Console.WriteLine($"{dataReader["TenSanpham"], 10} Gia {dataReader["Gia"], 10}");
        }
<br/>

- Output console:

    + ```
        server=localhost;database=tyanlab;user id=root;password=abc123;port=3307
        Bia 333 Gia  300000,00
        Nước ngọt Coca cola Gia  200000,00
        Tương Ớt Chin-Su (250g) Gia   12000,00
        Dầu Đậu Nành Simply Gia  247000,00
        Bột cần tây sấy lạnh Gia  145000,00
        Giấm táo hữu cơ Organic Bragg Gia  136000,00
        Mít sấy thăng hoa Gia   30000,00
        Tương ớt Thái Long Hảo Hạng 250gr Gia   25000,00
        Xúc Xích Dinh Dưỡng Thịt Heo Ponnie Gia   16000,00
        Tôm thịt đông lạnh size 31/40 Đôi Đũa Vàng gói 450g Gia  150000,00
<br/>

# SqlParameters
- Truy vấn kèm điều kiện là 1 giá trị có thể thay đổi:

    + ```
        using var command = new MySqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT DanhmucID, TenDanhMuc, MoTa FROM Danhmuc WHERE DanhmucID >= @value;";

        var danhMucID = new MySqlParameter("@value", 4);
        command.Parameters.Add(danhMucID);

        danhMucID.Value = 2;
    + Sau khi chạy lệnh `command.Parameters.Add()` thì cập nhật giá trị của biến được `Add` thoải mái bằng `danhMucID.Value`

- Có thể `Add` trực tiếp bằng `AddWithValue` 
    + ```
        var danhMucID = command.Parameters.AddWithValue("@value", 6);

