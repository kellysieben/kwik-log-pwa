namespace app.Services;

public interface IRepository<T>
{
    public Task<ICollection<T>> GetAllByOwnerAsync(string ownerId);
    public Task Add(T entry);
}