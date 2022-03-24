using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PickPoint.Lib.Extensions;
using PickPoint.Lib.Helpers.Configuration;
using PickPoint.Lib.Settings;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class SettingsContextsBuilderExtensions
{
    public static IHostBuilder UseSettingsContext(this IHostBuilder builder, string path)
    {
        return builder.ConfigureAppConfiguration((_, config) =>
        {
            config.Add<CustomConfigurationSource>(s =>
                s.Stream = File.Exists(path) ? File.OpenRead(path) : GetDefaultConfig());
        });
    }

    private static Stream GetDefaultConfig()
    {
        var settings = new PickPointAppSettings();
            
        settings.Init();
            
        var json = JsonConvert.SerializeObject(settings, JsonSerializationExtensions.JsonSerializationOptions);

        return new MemoryStream(Encoding.UTF8.GetBytes(json));
    }
}