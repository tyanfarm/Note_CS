namespace EF {
    public class CategoryDetail {
        public int CategoryDetailID {get; set;}

        public int UserId {get; set;}

        public DateTime CreatedDate {get; set;}

        public DateTime UpdatedDate {get; set;}

        public int CountProduct {get; set;}

        public virtual Category category {get; set;}
    }
}