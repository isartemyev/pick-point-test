using Serilog.Events;

namespace PickPoint.Lib.Settings;

public class LoggingSettings
{
    public string InitiatorName { get; set; }
        
    public string LogDirectory { get; set; }
        
    public string ArchiveLogDirectory { get; set; }
        
    public long LogSizeLimit { get; set; }
        
    public LogEventLevel LogLevel { get; set; }
        
    public long ArchiveDirectorySizeLimit { get; set; }
        
    public string OutputTemplate { get; set; }

    public void Init()
    {
        InitiatorName             = "PickPoint-RestApi";
        LogDirectory              = "/var/pickpoint/restapi/logs";
        ArchiveLogDirectory       = "/var/pickpoint/restapi/logs/archive";
        LogSizeLimit              = 1024 * 1024 * 50L;
        LogLevel                  = LogEventLevel.Verbose;
        ArchiveDirectorySizeLimit = 1024 * 1024 * 1024 * 10L;
        OutputTemplate            = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] {Message:lj}{NewLine}{Exception}";
    }
}