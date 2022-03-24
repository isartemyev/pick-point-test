using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PickPoint.RestApi.Service;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class WebHostDefaultsBuilderExtensions
{
    public static IHostBuilder UseWebHostDefaults(this IHostBuilder builder, int port) => builder.ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();

        webBuilder.UseKestrel(options =>
        {
            options.Limits.MaxRequestBodySize               = null;
            options.Limits.MaxConcurrentConnections         = 1000;
            options.Limits.MaxConcurrentUpgradedConnections = 1000;
            options.ListenAnyIP(port);
        });
    });
}