<div style="text-align: center; background-color: #FFBFBF; font-family: 'Trebuchet MS', Arial, sans-serif; color:  #0D0907; padding: 5px; font-size: 40px; padding:20px; font-weight: bold; border-radius: 0 0 0 0; box-shadow: 0px 6px 8px rgba(0, 0, 0, 0.2);margin-bottom: 20px;">
DOTNET NEW MVC
</div>

# UseRazorPage()
- `builder.Services.AddRazorPages();`
- `app.MapRazorPages();`

# services.addTransient()
- Inject các dịch vụ vào chương trình

- ```
    public string Logging() {
        _logger.LogWarning("CON CAC");
        _logger.LogError("CON CAC");
        _logger.LogDebug("CON CAC");
        _logger.LogCritical("CON CAC");
        _logger.LogInformation("CON CAC");

        return "CC";
    }

# Tool
- `dotnet tool install -g dotnet-aspnet-codegenerator`: Giúp phát sinh các thư mục, controllers, views, razor page nhanh hơn

- `dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design`: Thêm package

- `dotnet asp-codegenerator -h`: xem lệnh

# Area
- `dotnet asp-codegenerator area` `NameArea`: tạo ra thư mục `Areas/NameArea/`
