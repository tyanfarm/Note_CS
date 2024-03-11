Console.WriteLine("Start App");
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Cho phép sử dụng được file img, js, css
app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");

app.Run();
