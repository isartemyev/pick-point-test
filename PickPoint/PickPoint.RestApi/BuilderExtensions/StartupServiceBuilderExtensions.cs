using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.RestApi.Service;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class StartupServiceBuilderExtensions
{
    public static IHostBuilder UseStartupService(this IHostBuilder builder) => builder.ConfigureServices(collection =>
    {
        collection.AddHostedService<StartupService>();
    });
}