using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace EF {
    public class ShopContext : DbContext {
        // Dịch vụ Logging
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();
        });

        // DbSet tương ứng với 1 TABLE trong CSDL
        public DbSet<Product> products {get; set;}
        public DbSet<Category> categories {get; set;}

        private const string connectionString = "server=127.0.0.1;database=tyanlab1;user id=root;password=abc123;port=3307";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseMySQL(connectionString);
        }
    }
}