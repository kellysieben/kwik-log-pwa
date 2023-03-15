using common;

namespace app.Pages;

public partial class Index
{

    private ICollection<KwikLogDTO> entries;

    protected override async Task OnInitializedAsync()
    {
        System.Console.WriteLine($"JBF.cs : Index.OnInitializedAsync");
        entries = await _logRepository.GetAllAsync();
    }

    private string CalcDeltaDays(KwikLogDTO entry)
    {
        var delta = DateTime.UtcNow - entry.TimestampUTC;
        return $"{Math.Floor(delta.TotalDays)}";
    }
}