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

## 
