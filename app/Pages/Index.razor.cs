using common;

namespace app.Pages;

public partial class Index
{

    private ICollection<KwikLogDTO> entries;

    protected override async Task OnInitializedAsync()
    {
        entries = await _logRepository.GetAllAsync();
        var jbf_1 = 1;
    }
}