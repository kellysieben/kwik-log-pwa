using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using app;
using app.Services;
using common;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IRepository<KwikLogDTO>, AppLogsRepo>();
builder.Services.AddScoped<IndexedDbAccessor>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.LoginMode = "Redirect";
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
});

System.Console.WriteLine($"JBF.cs : Program.01");

// Make sure all DI services are setup before this point
//await builder.Build().RunAsync();

var host = builder.Build();
using var scope = host.Services.CreateScope();
await using var indexedDB = scope.ServiceProvider.GetService<IndexedDbAccessor>();

System.Console.WriteLine($"JBF.cs : Program.02");

if (indexedDB is not null)
{
    await indexedDB.InitializeAsync();
}

await host.RunAsync();
