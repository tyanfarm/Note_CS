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

CreateDatabase();