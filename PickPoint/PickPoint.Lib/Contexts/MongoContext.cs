using MongoDB.Driver;

namespace PickPoint.Lib.Contexts;

public class MongoContext
{
    private const string AuthMechanism = "SCRAM-SHA-1";

    public readonly IMongoDatabase DataBase;

    public MongoContext(string connectionString)
    {
        var connection = new MongoUrlBuilder(connectionString);

        var internalIdentity = new MongoInternalIdentity(connection.DatabaseName, connection.Username);
        var passwordEvidence = new PasswordEvidence(connection.Password);
        var mongoCredential  = new MongoCredential(AuthMechanism, internalIdentity, passwordEvidence);
        var settings         = new MongoClientSettings
        {
            Credential = mongoCredential
        };

        var address = new MongoServerAddress(connection.Server.Host, connection.Server.Port);

        settings.Server = address;

        var client = new MongoClient(settings);

        DataBase = client.GetDatabase(connection.DatabaseName);
    }
}