using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.RollingFileSizeLimit.Extensions;
using Serilog.Sinks.RollingFileSizeLimit.Impl;

namespace PickPoint.Lib.Extensions
{
    public static class LoggerExtensions
    {
        public static LoggerConfiguration Default(this LoggerConfiguration configuration, 
            LogEventLevel level,
            string outputTemplate,
            string logDirectory,
            string archiveDirectory,
            long fileSizeLimitBytes,
            long archiveSizeLimitBytes,
            string logFilePrefix)
        {
            return configuration
                .Enrich.WithProcessName()
                .Enrich.WithProcessId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .MinimumLevel.Is(level)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.Console(outputTemplate: outputTemplate)
                .WriteTo.RollingFileSizeLimited(logDirectory,
                    archiveDirectory,
                    fileSizeLimitBytes: fileSizeLimitBytes,
                    archiveSizeLimitBytes: archiveSizeLimitBytes,
                    logFilePrefix: logFilePrefix,
                    fileCompressor: new DefaultFileCompressor(),
                    outputTemplate: outputTemplate);
        }
    }
}