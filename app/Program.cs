using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using app;
using app.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.ConfigureServices();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

System.Console.WriteLine($"JBF.cs : Program.01");

//await builder.Build().RunAsync();
var host = builder.Build();
using var scope = host.Services.CreateScope();
await using var indexedDB = scope.ServiceProvider.GetService<IndexedDbAccessor>();

System.Console.WriteLine($"JBF.cs : Program.02");

if (indexedDB is not null)
{
    await indexedDB.InitializeAsync();
}

System.Console.WriteLine($"JBF.cs : Program.03");

await host.RunAsync();
