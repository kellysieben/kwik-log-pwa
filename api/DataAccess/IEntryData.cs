using common;

namespace api.DataAccess;

public interface IEntryData
{
    Task Add(KwikLogDTO entry);
    Task<List<KwikLogDTO>> GetAllByOwner(string ownerId);
}