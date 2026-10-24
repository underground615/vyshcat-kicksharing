using System.Text;
using Microsoft.Extensions.DependencyInjection;
using VyshCat;
using VyshCat.Services;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var services = new ServiceCollection();
services.AddSingleton<IServiceCenter, ServiceCenter>();
services.AddSingleton<FleetService>();
services.AddTransient<ConsoleMenu>();
using var provider = services.BuildServiceProvider();
var menu = provider.GetRequiredService<ConsoleMenu>();

bool demo = false;
foreach (var arg in args) {
    if (arg == "--demo") {
        demo = true;
    }
}

if (demo) {
    menu.RunDemo();
}
else {
    menu.Run();
}
