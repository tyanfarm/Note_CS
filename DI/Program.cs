using CS_Interface;
using CS_MySerVice;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// // IClassC temp = new ClassC()
// services.AddSingleton<Interface.IClassC, Interface.ClassC>();
// services.AddSingleton<Interface.IClassB>(Interface.CreateB);


// Interface.IClassB? b = providers.GetService<Interface.IClassB>();

// MyService
services.Configure<MyServiceOptions>(
    (MyServiceOptions options) => {
        options.data1 = "Bonjour";
        options.data2 = 2024;
    }
);

services.AddSingleton<MyService>();


var providers = services.BuildServiceProvider();

var myservice = providers.GetService<MyService>();

myservice?.PrintData();

