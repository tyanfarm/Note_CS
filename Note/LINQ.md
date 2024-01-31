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

## GroupBy use Query
- ```
    var result = from p in products
                group p by p.Price into gr
                orderby gr.Key
                select gr;

    result.ToList().ForEach(group => {
        Console.WriteLine(group.Key);

        group.ToList().ForEach(p => Console.WriteLine(p));
    });
- Console:
    ```
    200
    1   Laptop       200   3   Yellow, Green
    300
    2   Table        300   1   Orange, Purple
    400
    4   Door         400   3   Blue
    700
    3   Fan          700   2   Yellow
    800
    5   Microwave    800   2   Yellow, Green
    1000
    7   TV           1000  3   Yellow, Grey
    2000
    6   Fridge       2000  1   Red
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

## Distinct
- Lọc lấy cái giá trị `unique` từ `SelectMany`
- ```
    products.SelectMany(p => p.Colors).Distinct().ToList().ForEach(color => Console.WriteLine(color));
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

## GroupJoin
- Khác `Join` ở chỗ `Join` tạo bảng dựa trên 2 cột có giá trị bằng nhau. `GroupJoin` tạo bảng và nhóm riêng (tương tự với `GroupBy` trong `SQL`)

- ```
    var result = brands.GroupJoin(products, b => b.ID, p => p.Brand, (b, p) => {
        return new {
            Brand = b.Name,
            Products = p
        };
    });

    foreach (var product in result) {
        Console.WriteLine(product.Brand);
        foreach (var item in product.Products) {
            Console.WriteLine(item);
        }
    }
- Console:
    ```
    A Company
        2   Table        300   1   Orange, Purple
        6   Fridge       2000  1   Red
    B Company
        3   Fan          700   2   Yellow
        5   Microwave    800   2   Yellow, Green
    C Company
        1   Laptop       200   3   Yellow, Green
        4   Door         400   3   Blue
        7   TV           1000  3   Yellow, Grey
<br/>

## GroupJoin & Join
- When you Join the two lists on the `Id` field the result will be:
    ```
    Value ChildValue
    A     a1
    A     a2
    A     a3
    B     b1
    B     b2
- When you GroupJoin the two lists on the `Id` field the result will be:
    ```
    Value  ChildValues
    A      [a1, a2, a3]
    B      [b1, b2]
    C      []

## GroupBy
- `GroupBy` theo `Price`

- ```
    var result = products.GroupBy(p => p.Price);

    foreach (var group in result) {
        Console.WriteLine(group.Key);

        foreach (var item in group) {
            Console.WriteLine(item);
        }
    }
- Console:
    ```
    200
    1   Laptop       200   3   Yellow, Green
    300
    2   Table        300   1   Orange, Purple
    700
    3   Fan          700   2   Yellow
    400
    4   Door         400   3   Blue
    800
    5   Microwave    800   2   Yellow, Green
    2000
    6   Fridge       2000  1   Red
    1000
    7   TV           1000  3   Yellow, Grey
<br/>

## Take
- Bắt đầu từ phần tử đầu tiên
- ```
    products.Take(4).ToList().ForEach(p => Console.WriteLine(p));
- Console:
    ```
    ID  Name        Price  Brand     Colors
    1   Laptop       200    3        Yellow, Green
    2   Table        300    1        Orange, Purple
    3   Fan          700    2        Yellow
    4   Door         400    3        Blue
<br/>

## Skip
- Bắt đầu từ phần tử đầu tiên
- ```
    products.Skip(3).ToList().ForEach(p => Console.WriteLine(p));
- Console:
    ```
    4   Door         400   3   Blue
    5   Microwave    800   2   Yellow, Green
    6   Fridge       2000  1   Red
    7   TV           1000  3   Yellow, Grey
<br/>

## OrderBy 
- Tăng dần
- ```
    products.OrderBy(p => p.Price).ToList().ForEach(p => Console.WriteLine(p));
- Console:
    ```
    1   Laptop       200   3   Yellow, Green
    2   Table        300   1   Orange, Purple
    4   Door         400   3   Blue
    3   Fan          700   2   Yellow
    5   Microwave    800   2   Yellow, Green
    7   TV           1000  3   Yellow, Grey
    6   Fridge       2000  1   Red
<br/>

## OrderByDescending
- Giảm dần
- ```
    products.OrderByDescending(p => p.Brand).ToList().ForEach(p => Console.WriteLine(p));
- Console:
    ```
    1   Laptop       200   3   Yellow, Green
    4   Door         400   3   Blue
    7   TV           1000  3   Yellow, Grey
    3   Fan          700   2   Yellow
    5   Microwave    800   2   Yellow, Green
    2   Table        300   1   Orange, Purple
    6   Fridge       2000  1   Red
<br/>

## SingleOrDefault
- Nếu giá trị nhiều hơn 1 thì trả về null

- ```
    var result = products.SingleOrDefault(p => p.Price == 200);
    if (result != null) {
        Console.WriteLine(result);
    }
<br/>

## Any
- Kiểm tra có tồn tại 1 giá trị nào đó thỏa điều kiện (trả về `True`/ `False`)

- ```
    var result = products.Any(p => p.Price == 500);
    
    Console.WriteLine(result);
<br/>

## All
- Kiểm tra tất cả giá trị thỏa điều kiện không (trả về `True`/ `False`)

- ```
    var result = products.All(p => p.Price >= 600);
    
    Console.WriteLine(result);
<br/>

## Count
- Đếm các giá trị thỏa 1 điều kiện nào đó

- ```
    var result = products.Count(p => p.Price >= 600);
    
    Console.WriteLine(result);
<br/>

## Example using LINQ
- ```
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
- Console:
    ```
    Fridge     A Company  2000 
    TV         C Company  1000 
    Microwave  B Company  800
    Fan        B Company  700
<br/>


