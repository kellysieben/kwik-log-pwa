using common;
using MongoDB.Driver;

namespace api.DataAccess;

public class MongoDBConnection : IDBConnection
{
    private readonly IConfiguration _config;
    private readonly IMongoDatabase _db;
    private string _connectionId = "MongoDB";
    public string DBName { get; private set; }
    public string EntryCollectionName { get; private set; } = "entries";
    public string UserCollectionName { get; private set; } = "users";
    public IMongoClient Client { get; private set; }
    public IMongoCollection<KwikLogDTO> EntryCollection { get; private set; }

    public MongoDBConnection(IConfiguration config)
    {
        _config = config;
        Client = new MongoClient(_config.GetConnectionString(_connectionId));

        DBName = _config["DatabaseName"] ?? "uhoh_dbname_not_in_config";
        _db = Client.GetDatabase(DBName);

        EntryCollection = _db.GetCollection<KwikLogDTO>(EntryCollectionName);
    }
}