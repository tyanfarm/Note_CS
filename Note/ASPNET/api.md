<div style="text-align: center; background-color: #FFBFBF; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 40px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
WEB API
</div>

# Connect Database
- `MySQL`:
    + cài đặt package `dotnet add package MySql.Data --version 8.3.0` (version có thể tùy chỉnh)
    + Connection String:
        + ```
            "ConnectionStrings": {
                "DefaultConnection": "server=127.0.0.1;uid=root;pwd=tyan;database=testdb;port=3306"
            }
        + String này sẽ dùng lệnh kết nối sau:
        + ```
            string query = @"SELECT DepartmentID, DepartmentName from Department";

            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource))

# Routing
- Khi cài đặt attribute `Route` ở các `Controller` thì để Routing được trên web ta cần thêm `app.MapControllers()` ở `Program.cs`

- `ApiController`: 
    + Tự động trả về các `HTTP Status Code` của các action có attribute `HttpGet`, `HttpPost`, ...

    + Tự động kiểm tra `ModelState` và trả về `Bad Request - 400` nếu không hợp lệ.
        + `ModelState` thường được sử dụng để kiểm tra tính hợp lệ của dữ liệu

- `HttpGet`: khi truy cập đến `controller` thì `action` có thuộc tính này sẽ tự động được chạy

