namespace PickPoint.Lib.Settings;

public class DataSourceSettings
{
    public string ProdConnectionString { get; set; }

    public string DevConnectionString { get; set; }
        
    private void Init()
    {
        ProdConnectionString  = "mongodb://pickpoint-user:passw0rd@45.84.226.29:27017/pickpoint-database";
        DevConnectionString   = "mongodb://pickpoint-user:passw0rd@45.84.226.29:27017/pickpoint-database";
    }
}