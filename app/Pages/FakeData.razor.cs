using System.ComponentModel.DataAnnotations;
using common;
using app.Services;

namespace app.Pages;

public class AddEntry
{
    [Required]
    public string Entry { get; set; }
}

public partial class FakeData
{
    private AddEntry addEntry = new();
    private DateTime lastAdd = DateTime.MinValue;
    protected override async Task OnInitializedAsync()
    {
        await Task.Run(() => System.Console.WriteLine($"JBF.cs : FakeData.OnInitializedAsync"));
    }

    private async Task AddFakeData()
    {
        KwikLogDTO logEntry = new()
        {
            Entry = addEntry.Entry,
            TimestampUTC = DateTime.UtcNow
        };

        await IndexedDbAccessor.SetValueAsync<KwikLogDTO>("entries", logEntry);

        System.Console.WriteLine($"JBF.cs : FakeData.AddFakeData @ {logEntry.TimestampUTC}");
        addEntry = new();
        lastAdd = logEntry.TimestampUTC.ToLocalTime();
    }
}
