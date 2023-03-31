using common;

namespace app.Services;

public class AppLogsRepo : IRepository<KwikLogDTO>
{
    private IndexedDbAccessor _indexedDbAccessor;

    public AppLogsRepo(IndexedDbAccessor indexedDbAccessor)
    {
        _indexedDbAccessor = indexedDbAccessor;
    }

    public async Task Add(KwikLogDTO entry)
    {
        await _indexedDbAccessor.SetValueAsync<KwikLogDTO>("entries", entry);
    }

    public async Task<ICollection<KwikLogDTO>> GetAllAsync(string oid)
    {
        return await _indexedDbAccessor.GetAllByOwnerAsync<KwikLogDTO>("entries", oid);
    }
}