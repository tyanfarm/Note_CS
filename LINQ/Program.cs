using System.Linq;
using DataProduct;

namespace LINQ {
    class Program {
        static void Main(string[] args) {
            var brands = new List<Brand> {
                new Brand {ID = 1, Name= "A Company"},
                new Brand {ID = 2, Name= "B Company"},
                new Brand {ID = 3, Name= "C Company"},
            };

            var products = new List<Product> {
                new Product(1, "Laptop", 200, new string[] {"Yellow", "Green"}, 3),
                new Product(2, "Table", 300, new string[] {"Orange", "Purple"}, 1),
                new Product(3, "Fan", 700, new string[] {"Yellow"}, 2),
                new Product(4, "Door", 400, new string[] {"Blue"}, 3),
                new Product(5, "Microwave", 800, new string[] {"Yellow", "Green"}, 2),
                new Product(6, "Fridge", 2000, new string[] {"Red"}, 1),
                new Product(7, "TV", 1000, new string[] {"Yellow", "Grey"}, 3),
            };

            products.Where(p => p.Price >= 500 && p.Price <= 2000)
                    .OrderByDescending(p => p.Price)
                    .Join(brands, p => p.Brand, b => b.ID, (p, b) => {
                        return new {
                            Name_Product = p.Name,
                            Name_Brand = b.Name,
                            Price = p.Price
                        };
                    })
                    .ToList()
                    .ForEach(item => {
                        Console.WriteLine($"{item.Name_Product, -10} {item.Name_Brand, -10} {item.Price, -5}");
                    });
            

            // foreach (var group in result) {
            //     Console.WriteLine(group.Key);

            //     foreach (var item in group) {
            //         Console.WriteLine(item);
            //     }
            // }



        }
    }
}
