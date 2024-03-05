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
<br/>

# CRUD with database
- ```
    static void CreateDatabase() {
        using var dbcontext = new ProductDbContext();
        string dbname = dbcontext.Database.GetDbConnection().Database;

        // Create Database
        var result = dbcontext.Database.EnsureCreated();

        if (result) {
            Console.WriteLine($"Create db {dbname} successfully!");
        }
        else {
            Console.WriteLine("ERROR!");
        }
    }
- ```
    static void DropDatabase() {
        using var dbcontext = new ProductDbContext();
        string dbname = dbcontext.Database.GetDbConnection().Database;

        // Delete database
        var result = dbcontext.Database.EnsureDeleted();

        if (result) {
            Console.WriteLine($"Delete db {dbname} successfully!");
        }
        else {
            Console.WriteLine("ERROR!");
        }
    }
- ```
    static void InsertDatabase() {
        using var dbcontext = new ProductDbContext();

        var products = new Product[] {
            new Product() {ProductId = 1, ProductName = "Product 1", Provider = "Company A"},
            new Product() {ProductId = 6, ProductName = "Product 2", Provider = "Company B"},
            new Product() {ProductId = 7, ProductName = "Product 3", Provider = "Company C"},
            new Product() {ProductId = 8, ProductName = "Product 4", Provider = "Company D"},
            new Product() {ProductId = 9, ProductName = "Product 5", Provider = "Company E"},
        };

        // AddRange để thêm nhiều đối tượng
        dbcontext.AddRange(products);

        int num_rows = dbcontext.SaveChanges();

        Console.WriteLine($"Insert {num_rows} rows !");
    }
- ```
    static void ReadDatabase() {
        using var dbcontext = new ProductDbContext();

        // LINQ
        
        // var products = dbcontext.products.ToList();
        // products.ForEach(product => product.PrintInfo());

        // products là 1 DbSet
        var result = from product in dbcontext.products
                    where product.ProductId >= 4
                    select product;

        result.ToList().ForEach(product => product.PrintInfo());
    }
- ```
    static void RenameProduct(int id, string newName) {
        using var dbcontext = new ProductDbContext();

        Product result = (from product in dbcontext.products
                    where product.ProductId == id
                    select product).FirstOrDefault();

        if (result != null) {
            result.ProductName = newName;
            
            int num_row = dbcontext.SaveChanges();
            Console.WriteLine($"{num_row} rows has been changed !");
        }
    }
- ```
    static void DeleteProduct(int id) {
        using var dbcontext = new ProductDbContext();

        Product result = (from product in dbcontext.products
                    where product.ProductId == id
                    select product).FirstOrDefault();

        if (result != null) {
            dbcontext.Remove(result);
            
            int num_row = dbcontext.SaveChanges();
            Console.WriteLine($"Delete {num_row} rows !");
        }
    }

# Logging
- Install Package:
    + ```
        dotnet add package Microsoft.Extensions.DependencyInjection
        dotnet add package Microsoft.Extensions.Logging
        dotnet add package Microsoft.Extensions.Logging.Console
- Để sử dụng `Logging` thì ta cần khai báo dịch vụ trong `DbContext`
    + ```
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();
        });
- Trong `Onfiguring()`:
    + ```
        optionsBuilder.UseLoggerFactory(loggerFactory);
<br/>

# FOREIGN KEY (Khóa ngoại)
- `Primary Key (PK)` tương ứng với thuộc tính `[Key]`
- `Foreign Key (FK)`: một `column` ánh xạ tới `PK` của một `TABLE` khác

- Ví dụ ta có 2 `TABLE`: `Category` và `Product`
    + ```
        [Table("Category")]
        public class Category {
            [Key]
            public int CategoryId {get; set;}

            [StringLength(50)]
            [Column("CategoryName", TypeName = "nvarchar(50)")]
            public string? Name {get; set;}

            [Column(TypeName = "nvarchar(200)")]
            public string? Description {get; set;}
        }
    + ```
        [Table("Product")]
        public class Product {
            [Key]
            public int ProductId {get; set;}

            [Required]
            [StringLength(50)]
            [Column("ProductName", TypeName = "nvarchar(50)")]
            public string? Name {get; set;}
            
            public decimal Price {get; set;}

            public int CateId {get; set;}

            // FOREIGN KEY
            [ForeignKey("CateId")]
            public Category? category {get; set;}       // FK -> PK

            public void PrintInfo() => Console.WriteLine($"{ProductId} - {Name} - {Price}");
        }
    + `FK` trong bảng `Product` ánh xạ qua `Category` là `CategoryId` của `Category`. 
    
    + Nếu ghi `public Category? category {get; set;}` với `[ForeignKey("CateId")]` thì trong bảng `Product` sẽ có thêm 1 cột `CateId`.
    
    + `public int CateId {get; set;}`: Cài đặt sẵn 1 cột `CateId` tương ứng với `FK` thì khi khai báo `FK` như ở trên hệ thống sẽ tự hiểu đã có sẵn cột `CateId` và không tạo thêm cột mới.

- Insert dữ liệu tương ứng:
    + Với `int CateId`: 
        + ```
            dbcontext.Add(new Product() {ProductId = 6, Name = "Axus Vivobook", Price = 13900000, CateId = 2});
    + Với `Category? category`:
        + ```
            var c1 = (from c in dbcontext.categories where c.CategoryId == 1 select c).FirstOrDefault();
        + ```
            dbcontext.Add(new Product() {ProductId = 2, Name = "Thinkpad Ubuntu", Price = 20000000, category = c2});
<br/>

# Reference Navigaton - Tham chiếu tới dữ liệu bảng qua FK
- Trong một mối quan hệ một-nhiều giữa `Category` và `Product`, `Reference Navigation` sẽ là từ `Product` đến `Category`. Mỗi `Product` có một `Reference Navigation` đến `Category`.

- ```
    using var dbcontext = new ShopContext();

    var product = (from p in dbcontext.products where p.ProductId == 4 select p).FirstOrDefault();
    
    if (product.category != null) {
        Console.WriteLine($"{product.category.Name} - {product.category.Description}");
    } 
    else {
        Console.WriteLine("Category = Null");
    }
- Kết quả đoạn code trên sẽ ra `Category = Null` vì lúc này `product.category` chưa được load dữ liệu.

- ```
    var e = dbcontext.Entry(product);
    e.Reference(p => p.category).Load();
- `Entry` dùng để thực hiện các thao tác như cập nhật hoặc tải dữ liệu vào đối tượng.

- `e.Reference(p => p.category).Load()`: 
    + `Reference` được sử dụng khi có một mối quan hệ một-nhiều hoặc một-một giữa các bảng trong cơ sở dữ liệu.

    + Sau khi xác định mối quan hệ, `Load` được gọi để tải dữ liệu của thực thể `category` từ cơ sở dữ liệu vào bộ nhớ.

# Collection Navigation
- Đại diện cho mối quan hệ từ một thực thể đến một tập hợp các thực thể khác.

- Mỗi `Category` có một `Collection Navigation` chứa tất cả các `Product` thuộc danh mục đó.

- Hệ thống sẽ tìm trên danh sách các phần tử của `Product.CateId` (`FK`) sau đó chia nhóm để đưa vào `Category.products`

- ```
    using var dbcontext = new ShopContext();

    var category = (from c in dbcontext.categories where c.CategoryId == 1 select c).FirstOrDefault();
    Console.WriteLine($"{category.Name} - {category.Description}");

    var e = dbcontext.Entry(category);
    e.Collection(c => c.products).Load();

    if (category.products != null) {
        category.products.ForEach(p => p.PrintInfo());
    }
    else Console.WriteLine("products == null");
<br/>

# Inverse Property
- Khi `Product` có 2 `CateId` thì ta cần xác định xem `CateId` nào liên kết với `List<Product>` của `Category`.
- Cài đặt như sau:
    + ```
        public int CateId {get; set;}

        // FOREIGN KEY
        [ForeignKey("CateId")]
        [InverseProperty("products")]
        public Category? category {get; set;} 
<br/>

# Automatic Reference
- Add Package:
    + `dotnet add package Microsoft.EntityFrameworkCore.Proxies --version 6.0.0`

- Thêm `optionsBuilder.UseLazyLoadingProxies();` vào `Onfiguring()`

- Set `virtual` cho các đối tượng tham chiếu    
    + `public virtual List<Product> products {get; set;}`
    + `public virtual Category category {get; set;}`

- Nhờ `LazyLoad` này mà ta không cần phải `Entry` các đối tượng. Khi nào sử dụng tới đối tượng thì nó sẽ lấy dữ liệu về hợp lý.

# Các loại DELETE
- `ON DELETE CASCADE`: khi bạn xóa một hàng từ bảng B, tất cả các hàng trong bảng A mà có khóa ngoại tham chiếu đến khóa chính của hàng được xóa trong bảng B cũng sẽ bị xóa.

- `ON DELETE RESTRICT`: khi bạn cố gắng xóa một hàng từ bảng B mà có các hàng liên quan trong bảng A, một lỗi hoặc ngoại lệ sẽ được ném, và việc xóa sẽ không được thực hiện.

- `ON DELETE SET NULL`: tất cả các cột của bảng A mà có khóa ngoại tham chiếu đến khóa chính của hàng được xóa trong bảng B sẽ được đặt giá trị NULL.
