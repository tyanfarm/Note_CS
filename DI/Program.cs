using CS_Interface;
using CS_MySerVice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


IConfigurationRoot configurationRoot;

ConfigurationBuilder configBuilder = new ConfigurationBuilder();
configBuilder.SetBasePath(Directory.GetCurrentDirectory());
configBuilder.AddJsonFile("configuration.json");

configurationRoot = configBuilder.Build();

var data1 = configurationRoot.GetSection("MyServiceOptions").GetSection("data1").Value;
var data2 = configurationRoot.GetSection("MyServiceOptions").GetSection("data2").Value;

Console.WriteLine($"{data1} in {data2}");

// var services = new ServiceCollection();

// // IClassC temp = new ClassC()
// services.AddSingleton<Interface.IClassC, Interface.ClassC>();
// services.AddSingleton<Interface.IClassB>(Interface.CreateB);


// Interface.IClassB? b = providers.GetService<Interface.IClassB>();

// MyService
// services.Configure<MyServiceOptions>(
//     (MyServiceOptions options) => {
//         options.data1 = "Bonjour";
//         options.data2 = 2024;
//     }
// );

// services.AddSingleton<MyService>();


// var providers = services.BuildServiceProvider();

// var myservice = providers.GetService<MyService>();

// myservice?.PrintData();

