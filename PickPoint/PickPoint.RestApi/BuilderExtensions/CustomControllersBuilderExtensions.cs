using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.RestApi.Controllers;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class CustomControllersBuilderExtensions
{
    public static IHostBuilder UseCustomControllers(this IHostBuilder builder) => builder.ConfigureServices((context, services) =>
    {
        services.AddScoped<HomeController>();
        //services.AddScoped<CommonController>();
    });
}