using System.Linq;
using EF;
using Microsoft.EntityFrameworkCore;

static void CreateDatabase() {
    using var dbcontext = new ShopContext();
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
    using var dbcontext = new ShopContext();
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

static void InsertData() {
    using var dbcontext = new ShopContext();

    Category c1 = new Category() {CategoryId = 1, Name = "Phone", Description = "List of phones"};
    Category c2 = new Category() {CategoryId = 2, Name = "Laptop", Description = "List of laptops"};

    dbcontext.categories.Add(c1);
    dbcontext.categories.Add(c2);

    dbcontext.Add(new Product() {ProductId = 1, Name = "IPhone XS", Price = 6000000, category = c1});
    dbcontext.Add(new Product() {ProductId = 2, Name = "Thinkpad Ubuntu", Price = 20000000, category = c2});
    dbcontext.Add(new Product() {ProductId = 3, Name = "Macbook Air", Price = 24000000, category = c2});
    dbcontext.Add(new Product() {ProductId = 4, Name = "IPhone 14", Price = 30000000, category = c1});
    dbcontext.Add(new Product() {ProductId = 5, Name = "IPhone 13 Pro Max", Price = 24000000, category = c1});
    dbcontext.Add(new Product() {ProductId = 6, Name = "Axus Vivobook", Price = 13900000, CateId = 2});
    
    dbcontext.SaveChanges();
}

// DropDatabase();
// CreateDatabase();
// InsertData();

using var dbcontext = new ShopContext();

var result = from p in dbcontext.products
            join c in dbcontext.categories on p.CateId equals c.CategoryId
            select new {
                name = p.Name,
                category = c.Name,
                price = p.Price
            };

result.ToList().ForEach(r => Console.WriteLine(r));


// var category = (from c in dbcontext.categories where c.CategoryId == 1 select c).FirstOrDefault();

// dbcontext.Remove(category);
// dbcontext.SaveChanges();



// using var dbcontext = new ShopContext();

// var product = (from p in dbcontext.products where p.ProductId == 4 select p).FirstOrDefault();

// product.PrintInfo();

// // Tracking
// var e = dbcontext.Entry(product);

// // Load dữ liệu
// e.Reference(p => p.category).Load();

// if (product.category != null) {
//     Console.WriteLine($"{product.category.Name} - {product.category.Description}");
// } 
// else {
//     Console.WriteLine("Category = Null");
// }

// var category = (from c in dbcontext.categories where c.CategoryId == 1 select c).FirstOrDefault();
// Console.WriteLine($"{category.Name} - {category.Description}");

// var e = dbcontext.Entry(category);
// e.Collection(c => c.products).Load();

// if (category.products != null) {
//     category.products.ForEach(p => p.PrintInfo());
// }
// else Console.WriteLine("products == null");