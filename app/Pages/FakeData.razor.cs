using System.ComponentModel.DataAnnotations;
using common;
using app.Services;
using System.Text.RegularExpressions;

namespace app.Pages;

public class FakeAction
{
    [Required]
    public string Action { get; set; } = "";
}

public partial class FakeData
{
    private FakeAction action = new();
    private DateTime mostRecentTime = DateTime.MinValue;
    private string? mostRecentCommand;
    protected override async Task OnInitializedAsync()
    {
        await Task.Run(() => System.Console.WriteLine($"JBF.cs : FakeData.OnInitializedAsync"));
    }

    private async Task HandleAdHocCommand()
    {
        string pattern = @"^\w+";
        var currentCommand = Regex.Match(action.Action, pattern).ToString();
        // KwikLogDTO logEntry = new()
        // {
        //     Entry = addEntry.Entry,
        //     TimestampUTC = DateTime.UtcNow
        // };

        // await IndexedDbAccessor.SetValueAsync<KwikLogDTO>("entries", logEntry);

        // System.Console.WriteLine($"JBF.cs : FakeData.AddFakeData @ {logEntry.TimestampUTC}");
        // addEntry = new();
        // lastAdd = logEntry.TimestampUTC.ToLocalTime();
        mostRecentTime = DateTime.Now;
        mostRecentCommand = currentCommand;
        action = new();
    }
}
