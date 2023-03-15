using common;

namespace app.Services;

public class FakeLogsRepo : IRepository<KwikLogDTO>
{
    private static Random rnd = new();

    public async Task<ICollection<KwikLogDTO>> GetAllAsync()
    {
        return await Task.Run(GetAll);
    }

    private ICollection<KwikLogDTO> GetAll()
    {
        int nEntries = rnd.Next(2000, 3000);
        var result = new List<KwikLogDTO>();
        //var currentTicks = DateTime.UtcNow.Ticks;

        for (int i =0; i < nEntries; i++)
        {
            result.Add(new KwikLogDTO
            {
                Entry = $"This is random entry number {i}",
                TimestampUTC = DateTime.UtcNow.AddSeconds(-1 * rnd.Next(8640000))
            });
        }

        return result.OrderByDescending(r => r.TimestampUTC).ToList();
    }
}