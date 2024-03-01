using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace EF {
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
}