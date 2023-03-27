using common;

namespace app.Pages;

public partial class Index
{
    private string? _entry;
    private string? _oid;

    private ICollection<KwikLogDTO> entries = new List<KwikLogDTO>();

    protected override async Task OnInitializedAsync()
    {
        System.Console.WriteLine($"JBF.cs : Index.OnInitializedAsync");
        await LoadAndVerifyUser();
        entries = await _logRepository.GetAllAsync();
        entries = entries.OrderByDescending(e => e.TimestampUTC).ToList();
    }

    public async Task AddEntry()
    {
        KwikLogDTO logEntry = new()
        {
             Entry = _entry,
             TimestampUTC = DateTime.UtcNow,
             Oid = _oid
        };

        await _logRepository.Add(logEntry);

        _entry = "";
        entries.Add(logEntry);
        entries = entries.OrderByDescending(e => e.TimestampUTC).ToList();
    }

    private async Task LoadAndVerifyUser()
    {
        var authState = await authProvider.GetAuthenticationStateAsync();
        var jbf1 = authState.User;
        var jbf2 = jbf1.Claims;

        foreach(var c in authState.User.Claims)
        {
            System.Console.WriteLine($"JBF.cs : Claim : [{c.Type}]");
        }

        _oid = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("oid"))?.Value;      
        string? name = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;      
        string? jobTitle = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("jobTitle"))?.Value;      
        System.Console.WriteLine($"JBF.cs : Index.LoadAndVerifyUser : [{name}] : [{jobTitle}] : [{_oid}]");
    }

    private string CalcDeltaDays(KwikLogDTO entry)
    {
        var delta = DateTime.UtcNow - entry.TimestampUTC;
        return $"{Math.Floor(delta.TotalDays)}";
    }
}