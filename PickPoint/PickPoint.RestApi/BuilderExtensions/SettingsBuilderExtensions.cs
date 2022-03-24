using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickPoint.Lib.Helpers.Configuration;
using PickPoint.Lib.Settings;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class SettingsBuilderExtensions
{
    public static IHostBuilder UseSettings(this IHostBuilder builder) => builder.ConfigureServices((context, services) =>
    {
        services.AddOptions().Configure<PickPointAppSettings>(context.Configuration);
                    
        services
            .AddSingleton(_ => context.Configuration.GetSection(CustomConfigurationProvider.Key).Get<PickPointAppSettings>())
            .AddSingleton(_ => context.Configuration.GetSection(CustomConfigurationProvider.Key).Get<PickPointAppSettings>().DataSource)
            .AddSingleton(_ => context.Configuration.GetSection(CustomConfigurationProvider.Key).Get<PickPointAppSettings>().Logging);
    });
}