using System.Linq;
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

// RenameProduct(3, "Laptop Thinkpad");
// DeleteProduct(1);
InsertDatabase();