using common;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace app.Pages;

public partial class Index
{
    private string? _entry;
    private string? _ownerId;

    private ICollection<KwikLogDTO> entries = new List<KwikLogDTO>();

    protected override async Task OnInitializedAsync()
    {
        await LoadAndVerifyUser();
        entries = await _logRepository.GetAllByOwnerAsync(_ownerId);
        entries = entries.OrderByDescending(e => e.TimestampUTC).ToList();
    }

    // private async ValueTask<ItemsProviderResult<KwikLogDTO>> LoadLogEntries(ItemsProviderRequest request)
    // {
    //     var startIndex = request.StartIndex;
    //     var count = request.Count;

    //     // Simulate an async data retrieval from a data source
    //     await Task.Delay(200);
    //     entries = await _logRepository.GetAllByOwnerAsync(_ownerId);
    //     entries = entries.OrderByDescending(e => e.TimestampUTC).ToList();

    //     var result = entries.Skip(startIndex).Take(count).ToList();

    //     return new ItemsProviderResult<KwikLogDTO>(result, entries.Count);
    // }

    public async Task AddEntry()
    {
        KwikLogDTO logEntry = new()
        {
             Entry = _entry,
             TimestampUTC = DateTime.UtcNow,
             OwnerId = _ownerId
        };

        await _logRepository.Add(logEntry);

        _entry = "";
        entries.Add(logEntry);
        entries = entries.OrderByDescending(e => e.TimestampUTC).ToList();
    }

    private async Task LoadAndVerifyUser()
    {
        var authState = await authProvider.GetAuthenticationStateAsync();
        // var jbf1 = authState.User;
        // var jbf2 = jbf1.Claims;

        // foreach(var c in authState.User.Claims)
        // {
        //     System.Console.WriteLine($"JBF.cs : Claim : [{c.Type}]");
        // }

        _ownerId = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("oid"))?.Value;      
        string? name = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;      
        string? jobTitle = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("jobTitle"))?.Value;      
        System.Console.WriteLine($"JBF.cs : Index.LoadAndVerifyUser : [{name}] : [{jobTitle}] : [{_ownerId}]");
    }

    private string CalcDeltaDays(KwikLogDTO entry)
    {
        var delta = DateTime.UtcNow - entry.TimestampUTC;
        return $"{Math.Floor(delta.TotalDays)}";
    }
}