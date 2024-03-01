using EF;
using Microsoft.EntityFrameworkCore;

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

static void InsertDatabase() {
    using var dbcontext = new ProductDbContext();

    var products = new Product[] {
        new Product() {ProductId = 1, ProductName = "Product 1", Provider = "Company A"},
        new Product() {ProductId = 2, ProductName = "Product 2", Provider = "Company B"},
        new Product() {ProductId = 3, ProductName = "Product 3", Provider = "Company C"},
        new Product() {ProductId = 4, ProductName = "Product 4", Provider = "Company D"},
        new Product() {ProductId = 5, ProductName = "Product 5", Provider = "Company E"},
    };

    // AddRange để thêm nhiều đối tượng
    dbcontext.AddRange(products);

    int num_rows = dbcontext.SaveChanges();

    Console.WriteLine($"Insert {num_rows} rows !");
}

InsertDatabase();