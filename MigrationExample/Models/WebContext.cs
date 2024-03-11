using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MigrationExample {
    public class WebContext : DbContext {
        // Dịch vụ Logging
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();
        });

        public DbSet<Article> articles {get; set;}
        public DbSet<Tag> tag {get; set;}

        private const string connectionString = "server=127.0.0.1;database=webdb;user id=root;password=abc123;port=3307";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseMySQL(connectionString);
        }
    }
}