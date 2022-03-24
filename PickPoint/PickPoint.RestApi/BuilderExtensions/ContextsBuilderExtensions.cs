using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.Lib.Contexts;
using PickPoint.Lib.Settings;
using PickPoint.Lib.Helpers.Configuration;
using Serilog;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class ContextsBuilderExtensions
{
    public static IHostBuilder UseContexts(this IHostBuilder builder) => builder.ConfigureServices((context, services) =>
    {
        var serviceSettings = context.Configuration.GetSection(CustomConfigurationProvider.Key).Get<PickPointAppSettings>().DataSource;

        if (context.HostingEnvironment.IsDevelopment())
        {
            services.AddSingleton(_ => new MongoContext(serviceSettings.DevConnectionString));

            Log.Information("Development database is used");
        }
        else
        {
            services.AddSingleton(_ => new MongoContext(serviceSettings.ProdConnectionString));
                
            Log.Information("Production database is used");
        }
    });
}