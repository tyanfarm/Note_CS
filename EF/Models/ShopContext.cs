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
        public DbSet<CategoryDetail> categoryDetail {get; set;}

        private const string connectionString = "server=127.0.0.1;database=tyanlab1;user id=root;password=abc123;port=3307";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseMySQL(connectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FLUENT API

            // Thao tác với bảng Product
            modelBuilder.Entity<Product>(entity => {
                // Table Mapping
                entity.ToTable("Product");          // [Table("Product")]

                // PK
                entity.HasKey(p => p.ProductId);    // [Key]

                // Index
                // Index giúp tăng hiệu suất truy vấn dữ liệu trên `Price`
                entity.HasIndex(p => p.Price).HasDatabaseName("index-products-price");

                // Relative One-To-Many (FK for Reference)
                // CateId
                entity.HasOne(p => p.category)          // public virtual Category category {get; set;} 
                        .WithMany(c => c.products)     // [InverseProperty("products")] - Collection Navigation
                        .HasForeignKey("CateId")        // [ForeignKey("CateId")]
                        .OnDelete(DeleteBehavior.NoAction)      
                        .HasConstraintName("FK_Product_Category");      // Set name FK

                // CateId2
                entity.HasOne(p => p.category2)
                        .WithMany()
                        .HasForeignKey("CateId2")
                        .OnDelete(DeleteBehavior.Cascade); 
            });
            
            // Relative One-To-One
            modelBuilder.Entity<Category>(entity => {
                entity.HasOne(c => c.categoryDetail)
                        .WithOne(d => d.category)
                        .HasForeignKey<CategoryDetail>(d => d.CategoryDetailID)
                        .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}