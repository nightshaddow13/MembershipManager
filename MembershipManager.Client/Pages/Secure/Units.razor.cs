using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ServiceStack;

namespace MembershipManager.Client.Pages.Secure;

public partial class Units
{
    [Inject] public JsonApiClient? Client { get; set; }

    bool isNotesOpen = false;
    bool isSchoolsOpen = false;
    bool isEventsOpen = false;

    protected void OnNotesClicked(MouseEventArgs args)
    {
        isNotesOpen = true;
        StateHasChanged();
    }

    protected void OnEventsClicked(MouseEventArgs args)
    {
        isEventsOpen = true;
        StateHasChanged();
    }

    protected void OnSchoolsClicked(MouseEventArgs args)
    {
        isSchoolsOpen = true;
        StateHasChanged();
    }
}
