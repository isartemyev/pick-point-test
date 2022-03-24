namespace PickPoint.Lib.Settings;

public class DataSourceSettings
{
    public string ProdConnectionString { get; set; }

    public string DevConnectionString { get; set; }
        
    private void Init()
    {
        ProdConnectionString  = "mongodb://my-user1:passw0rd@62.113.96.183:27017/my-database";
        DevConnectionString   = "mongodb://my-user1:passw0rd@62.113.96.183:27017/my-database";
    }
}