using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using PickPoint.Lib.Extensions;
using PickPoint.Lib.Settings;
using PickPoint.RestApi.BuilderExtensions;

public static class Program
{
    private const int DefaultPort = 5757;
    private const string DefaultConfigName = "/var/pickpoint/restapi/settings/config.json";

    public static void Main(
        string configName = DefaultConfigName, 
        int port = DefaultPort
    )
    {
        if (string.IsNullOrWhiteSpace(configName))
        {
            throw new ArgumentException("Value cannot be null or whitespace", nameof(configName));
        }

        if (port <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(port));
        }
        
        var host = CreateWebHostBuilder(configName, port).Build();

        var settings = host.Services.GetService<PickPointAppSettings>();

        Log.Information("RestApi starting with settings:\r\n{@Settings}", settings.ToJson());

        host.Run();
    }
    
    private static IHostBuilder CreateWebHostBuilder(string configName = DefaultConfigName, int port = DefaultPort)
    {
        return Host.CreateDefaultBuilder()
            .UseSettingsContext(configName)
            .UseSettings()
            .UseWebHostDefaults(port)
            .UseLogger()
            .UseHelpers()
            .UseCustomControllers()
            .UseFilters()
            .UseStartupService()
            .UseContexts()
            .UseRepositories()
            .UseFacades()
            .UseMapping();
    }
}