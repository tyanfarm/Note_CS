using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationExample {
    public class Tag
    {
        [Key]
        [StringLength(20)]
        public string TagId {set; get;}
        [Column(TypeName="nvarchar(100)")]
        public string Content {set; get;}
    }
}