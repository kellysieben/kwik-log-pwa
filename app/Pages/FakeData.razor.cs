using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using common;

namespace app.Pages;

public class FakeAction
{
    [Required]
    public string Action { get; set; } = "";
}

public partial class FakeData
{
    private static Random rnd = new();
    private FakeAction action = new();
    private DateTime mostRecentTime = DateTime.MinValue;
    private string? mostRecentCommand;
    private string? _oid;
    protected override async Task OnInitializedAsync()
    {
        await Task.Run(() => System.Console.WriteLine($"JBF.cs : FakeData.OnInitializedAsync"));
        var authState = await authProvider.GetAuthenticationStateAsync();
        _oid = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("oid"))?.Value;
    }

    private async Task HandleAdHocCommand()
    {
        string pattern = @"^\w+";
        var currentCommand = Regex.Match(action.Action, pattern).ToString();

        await Run(currentCommand)(currentCommand, action.Action.Replace(currentCommand,"").Trim());

        mostRecentTime = DateTime.Now;
        mostRecentCommand = currentCommand;
        action = new();
    }

    private Func<string, string, Task> Run(string cmd) => 
        cmd switch 
        {
            "rnd" => Randos,
            _ => Unknown
        };

    private async Task Randos(string cmd, string args)
    {
        int nRandos = int.TryParse(args, out nRandos) ? nRandos : 0;
        for (int i = 0; i < nRandos; i++)
        {
            KwikLogDTO logEntry = new()
            {
                Entry = $"[rnd] Entry {i}-{rnd.Next(999)}",
                TimestampUTC = DateTime.UtcNow.AddHours(-1 * rnd.Next(8766)),
                Oid = _oid
            };

            await _logRepository.Add(logEntry);
        }
    }

    private async Task Unknown(string cmd, string args)
    {
        await Task.Run(() => System.Console.WriteLine($"JBF.cs : FakeData.Unknown cmd : [cmd]"));
    }
}
