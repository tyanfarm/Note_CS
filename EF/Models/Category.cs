using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF {
    [Table("Category")]
    public class Category {
        [Key]
        public int CategoryId {get; set;}

        [StringLength(50)]
        [Column("CategoryName", TypeName = "nvarchar(50)")]
        public string? Name {get; set;}

        [Column(TypeName = "nvarchar(200)")]
        public string? Description {get; set;}

        public virtual List<Product> products {get; set;}

        public virtual CategoryDetail categoryDetail {get; set;}
    }
}