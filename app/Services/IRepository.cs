namespace app.Services;

public interface IRepository<T>
{
    public Task<ICollection<T>> GetAllAsync(string oid);
    public Task Add(T entry);
}