<div style="text-align: center; background-color: skyblue; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 35px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
<div>LINQ</div>
<div>(Language Integrated Query)</div>
</div>

<br/>

# Basic Query 
- ```
    var query = from p in products 
                        where p.Price == 400
                        select p;

            foreach (var product in query) {
                Console.WriteLine(product);
            }
<br/>

# API LINQ
## Data
- ```
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
        new Product(5, "Microwave", 800, new string[] {"Yellow", "Green"}, 7),
        new Product(6, "Fridge", 2000, new string[] {"Red"}, 8),
        new Product(7, "TV", 1000, new string[] {"Yellow", "Grey"}, 5),
    };

## Select
- ```
    var result = products.Select(
                    // Delegate
                    (p) => {
                        return new {
                            Name = p.Name,
                            Price = p.Price
                        };
                    }
                );

    foreach (var product in result) {
        Console.WriteLine(product);
    }
<br/>

## Where
- ```
    var result = products.Where(
                    // Delegate
                    (p) => {
                        return p.Price >= 400 && p.Price <= 800;
                    }
                );

    foreach (var product in result) {
        Console.WriteLine(product);
    }
<br/>

## SelectMany
- Với `Colors` thì là 1 mảng `string` nên `Select` không xuất ra dữ liệu được nên ta sẽ dùng `SelectMany`

- ```
    var result = products.SelectMany(
                    // Delegate
                    (p) => {
                        return p.Colors;
                    }
                );
<br/>

## Min, Max, Sum, Average
- ```
    int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

    Console.WriteLine(numbers.Sum());
- ```
    Console.WriteLine(numbers.Where(n => n % 2 == 1).Sum());
- ```
    Console.WriteLine(products.Min(p => p.Price));
<br/>

## Join
- ```
    var result = products.Join(brands, p => p.Brand, b => b.ID, (p, b) => {
        return new {
            ID_Brand = p.Brand,
            Name = p.Name,
            Brand_Name = b.Name,
            ID = b.ID
        };
    });
- Console:
    ```
    { ID_Brand = 3, Name = Laptop, Brand_Name = C Company, ID = 3 }
    { ID_Brand = 1, Name = Table, Brand_Name = A Company, ID = 1 }
    { ID_Brand = 2, Name = Fan, Brand_Name = B Company, ID = 2 }
    { ID_Brand = 3, Name = Door, Brand_Name = C Company, ID = 3 }
    { ID_Brand = 2, Name = Microwave, Brand_Name = B Company, ID = 2 }
    { ID_Brand = 1, Name = Fridge, Brand_Name = A Company, ID = 1 }
    { ID_Brand = 3, Name = TV, Brand_Name = C Company, ID = 3 }
<br/>


