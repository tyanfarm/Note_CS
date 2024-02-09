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

var jsonMyService = configurationRoot.GetSection("MyServiceOptions");

//MyService
var services = new ServiceCollection();

services.Configure<MyServiceOptions>(jsonMyService);

services.AddSingleton<MyService>();

var providers = services.BuildServiceProvider();

var myservice = providers.GetService<MyService>();

myservice?.PrintData();


// // IClassC temp = new ClassC()
// services.AddSingleton<Interface.IClassC, Interface.ClassC>();
// services.AddSingleton<Interface.IClassB>(Interface.CreateB);


// Interface.IClassB? b = providers.GetService<Interface.IClassB>();



