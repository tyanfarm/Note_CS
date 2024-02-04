using CS_Interface;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// IClassC temp = new ClassC()
services.AddSingleton<Interface.IClassC, Interface.ClassC>();
services.AddSingleton<Interface.IClassB, Interface.ClassB>(
    (providers) => {
        var b2 = new Interface.ClassB(
            providers.GetService<Interface.IClassC>(),
            "Processing in Class B !"
        );

        return b2;
    }
);

var providers = services.BuildServiceProvider();

Interface.IClassB? b = providers.GetService<Interface.IClassB>(); 

b.ActionB();


