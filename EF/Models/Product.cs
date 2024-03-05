using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF {
    // class Product sẽ được ánh xạ vào TABLE MyProduct thay vì tên mặc định
    [Table("Product")]
    public class Product {
        [Key]
        public int ProductId {get; set;}

        [Required]
        [StringLength(50)]
        [Column("ProductName", TypeName = "nvarchar(50)")]
        public string? Name {get; set;}
        
        public decimal Price {get; set;}


        // Có `?` đồng nghĩa với `CAN BE NULL`
        public int CateId {get; set;}

        // FOREIGN KEY
        [ForeignKey("CateId")]
        [InverseProperty("products")]
        public virtual Category category {get; set;}       // FK -> PK


        public int? CateId2 {get; set;}

        // FOREIGN KEY
        [ForeignKey("CateId2")]
        public virtual Category category2 {get; set;}       // FK -> PK
        

        public void PrintInfo() => Console.WriteLine($"{ProductId} - {Name} - {Price} - {CateId}");
    }
}