<div style="text-align: center; background-color: lightsteelblue; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 40px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
Entity Framework
</div>

# Install Package
- ```
    dotnet add package System.Data.SqlClient

    dotnet add package MySql.EntityFrameworkCore --version 7.0.0

    dotnet add package Microsoft.EntityFrameworkCore

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer

    dotnet add package Microsoft.EntityFrameworkCore.Design

    dotnet add package Microsoft.Extensions.DependencyInjection

    dotnet add package Microsoft.Extensions.Logging

    dotnet add package Microsoft.Extensions.Logging.Console
# DataAdapter & EntityFramework
- `DataAdapter`:

    + Nó là một phần của mô hình disconnected data access, nơi dữ liệu được lấy ra từ cơ sở dữ liệu, làm việc với nó trong bộ nhớ và sau đó được cập nhật trở lại cơ sở dữ liệu.

    + `DataAdapter` được sử dụng để lấy dữ liệu từ CSDL và đổ dữ liệu vào trong các cấu trúc dữ liệu trong .NET như `DataTable` hoặc `DataSet`.

- `EntityFramwework`:

    + EF là một ORM (Object-Relational Mapping) cho .NET Framework, cho phép bạn làm việc với cơ sở dữ liệu bằng cách sử dụng các đối tượng và không cần phải viết các câu lệnh SQL cụ thể.

    + tiện lợi để ánh xạ các đối tượng trong mã .NET vào cơ sở dữ liệu, tự động tạo và cập nhật cấu trúc cơ sở dữ liệu dựa trên các lớp đối tượng.

- `DataAdapter` của `ADO.NET` sử dụng trong mô hình `disconnected data access`, còn `EF` là 1 `ORM` giúp làm việc qua các đối tượng trong .NET

# ProductDbContext
- Code:
    + ```
        public class ProductDbContext : DbContext {
            // DbSet tương ứng với 1 TABLE trong CSDL
            public DbSet<Product> products {get; set;}

            private const string connectionString = "server=127.0.0.1;database=tyanlab1;user id=root;password=abc123;port=3307";

            protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseMySQL(connectionString);
            }
        }
- `OnConfiguring()` trong DbContext
    + Sử dụng để cấu hình các tùy chọn cho DbContext

- Khi định nghĩa `DbSet<Product> products`, EF thao tác CRUD trên bảng Products trong CSQL qua `ProductDbContext`

- Ở class `Product` ta cài đặt thuộc tính `[Table("TableName")]` để class Product sẽ được ánh xạ vào TABLE MyProduct thay vì tên mặc định.

# LINQ & return Object
- ```
    Product result = (from product in dbcontext.products
                  where product.ProductId == id
                  select product);
- Lỗi xảy ra vì bạn đang cố gắng gán một biểu thức LINQ (một `IQueryable<Product>`) vào một biến `Product`.

- Để sửa lỗi này, bạn có thể sử dụng phương thức `FirstOrDefault()` hoặc `SingleOrDefault()` để trả về một đối tượng `Product`.

- ```
    Product result = (from product in dbcontext.products
                  where product.ProductId == id
                  select product).FirstOrDefault();



