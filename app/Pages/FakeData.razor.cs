using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using common;

namespace app.Pages;

public class FakeAction
{
    private const string Pattern = @"^\w+";

    [Required]
    public string Action { get; set; } = "";
    public string Cmd { get; private set; } = "";
    public string Args { get; private set; } = "";
    public string Result { get; private set; } = "";
    public DateTime ResultTime{ get; set; } = DateTime.Now;
    public string Oid { get; set; } = "";

    public void Parse(string oid)
    {
        Cmd = Regex.Match(Action, Pattern).ToString();
        Args = Action.Replace(Cmd,"").Trim();
        Oid = oid;
    }

    public void SetResult(string result)
    {
        Result = result;
        ResultTime = DateTime.Now;
    }
}

public record ActionLogEntry
{
    public string Cmd { get; private set; }
    public string Args { get; private set; }
    public string Result { get; private set; }
    public DateTime ResultTime{ get; private set; }
    public string Oid { get; private set; }

    public ActionLogEntry(FakeAction action)
    {
        Cmd = action.Cmd;
        Args = action.Args;
        Result = action.Result;
        ResultTime = action.ResultTime;
        Oid = action.Oid;
    }
}

public partial class FakeData
{
    private static Random rnd = new();
    private FakeAction action = new();
    private List<ActionLogEntry> _actionLog = new List<ActionLogEntry>();
    private string? _oid;
    protected override async Task OnInitializedAsync()
    {
        var authState = await authProvider.GetAuthenticationStateAsync();
        _oid = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("oid"))?.Value;
    }

    private async Task HandleAdHocCommand()
    {
        action.Parse(_oid);
        await Run(action)();
        _actionLog.Add(new ActionLogEntry(action));
        action = new();
    }

    private Func<Task> Run(FakeAction act) => 
        act.Cmd switch 
        {
            "rnd" => Randos,
            "del" => Del,
            _ => Unknown
        };

    private async Task Randos()
    {
        if (string.IsNullOrEmpty(action?.Action))
        {
            //action.SetResult("Empty Action");
            return;
        }

        int nRandos = int.TryParse(action.Args, out nRandos) ? nRandos : 0;
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

        action.SetResult("OK");
    }

    private async Task Unknown()
    {
        await Task.Run(() => action.SetResult("unknown"));
    }

    private async Task Del()
    {
        await Task.Run(() => action.SetResult("not implemented"));
    }
}
