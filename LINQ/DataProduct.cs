namespace DataProduct {
    public class Brand {
        public string? Name {get; set;}
        public int ID {get; set;}
    }

    public class Product {
        public int ID {get; set;}
        public string Name {get; set;}
        public double Price {get; set;}
        public string[] Colors {get; set;}
        public int Brand {get; set;}
        public Product(int id, string name, double price, string[] colors, int brand) {
            ID = id; 
            Name = name;
            Price = price;
            Colors = colors;
            Brand = brand;
        }

        // Vì mọi đối tượng đều kế thừa từ 'Object' nên đều có mặc định `ToString()`
        // Dùng overide để ghi đè phương thức
        override public string ToString() {
            return $"{ID, -3} {Name, -12} {Price, -5} {Brand, -3} {string.Join(", ", Colors)}";
        }
    }
}