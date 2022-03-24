namespace PickPoint.Lib.Settings;

public class PickPointAppSettings
{
    public DataSourceSettings DataSource { get; set; }

    public LoggingSettings Logging { get; set; }

    public void Init()
    {
        Logging = SettingsHelper.Init<LoggingSettings>();
        DataSource = SettingsHelper.Init<DataSourceSettings>();
    }
}