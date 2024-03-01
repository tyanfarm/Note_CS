using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF {
    // class Product sẽ được ánh xạ vào TABLE MyProduct thay vì tên mặc định
    [Table("MyProduct")]
    public class Product {
        [Key]
        public int ProductId {get; set;}

        [StringLength(50)]
        public string? ProductName {get; set;}
        
        [StringLength(50)]
        public string? Provider {get; set;}
    }
}