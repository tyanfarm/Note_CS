using CS_Interface;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// IClassC temp = new ClassC()
services.AddScoped<Interface.IClassC, Interface.ClassC>();

var providers = services.BuildServiceProvider();

for (int i = 0; i < 5; i++) {
    Interface.IClassC? c = providers.GetService<Interface.IClassC>();
    // Interface.IClassC c = new Interface.ClassC();

    Console.WriteLine(c?.GetHashCode()); 
}

using (var scope = providers.CreateScope()) {
    var provider1 = scope.ServiceProvider;

    for (int i = 0; i < 5; i++) {
        Interface.IClassC? c = provider1.GetService<Interface.IClassC>();
        // Interface.IClassC c = new Interface.ClassC();

        Console.WriteLine(c?.GetHashCode()); 
    }
}
 


