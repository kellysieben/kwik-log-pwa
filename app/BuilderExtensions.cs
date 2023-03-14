using app.Services;
using common;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace app;

public static class BuilderExtensions
{
    public static void ConfigureServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddSingleton<IRepository<KwikLogDTO>, FakeLogsRepo>();
    }
}