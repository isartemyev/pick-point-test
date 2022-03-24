using Microsoft.Extensions.Hosting;
using PickPoint.Lib.Extensions;
using PickPoint.Lib.Helpers.Configuration;
using PickPoint.Lib.Settings;
using Serilog;

namespace PickPoint.RestApi.BuilderExtensions;

internal static class LoggerBuilderExtensions
{
    public static IHostBuilder UseLogger(this IHostBuilder builder) => builder.UseSerilog((context, logger) =>
    {
        var loggingSettings = context.Configuration.GetSection(CustomConfigurationProvider.Key).Get<PickPointAppSettings>().Logging;

        logger
            .Default(loggingSettings.LogLevel,
                loggingSettings.OutputTemplate,
                loggingSettings.LogDirectory,
                loggingSettings.ArchiveLogDirectory,
                loggingSettings.LogSizeLimit,
                loggingSettings.ArchiveDirectorySizeLimit,
                loggingSettings.InitiatorName);
    });
}