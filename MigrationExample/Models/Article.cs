using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationExample {
    [Table("article")]
    public class Article
    {
        [Key]
        [StringLength(50)]
        public string ArticleId {set; get;}

        [StringLength(100)]
        public string Title {set;  get;}

    }
}