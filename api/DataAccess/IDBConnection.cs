using common;
using MongoDB.Driver;

namespace api.DataAccess;

public interface IDBConnection
{
    string DBName { get; }
    string EntryCollectionName { get; }
    string UserCollectionName { get; }
    IMongoClient Client { get; }
    IMongoCollection<KwikLogDTO> EntryCollection { get; }
}