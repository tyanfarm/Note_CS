<div style="text-align: center; background-color: #FFAE42; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 40px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
Migration
</div>

# Install Package
- ```
    dotnet add package System.Data.SqlClient
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.Extensions.DependencyInjection
    dotnet add package Microsoft.Extensions.Logging.Console
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet
- Install `dotnet ef`
    + ```
        dotnet tool install --global dotnet-ef
- List migrations:
    + ```
        dotnet ef migrations list
- Update database kể cả Migrations:
    + ```
        dotnet ef database update
- Drop database:
    + ```
        dotnet ef database drop -f
- Xóa migrations mới nhất:
    + ```
        dotnet ef migrations remove