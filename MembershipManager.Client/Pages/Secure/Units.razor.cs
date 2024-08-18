using MembershipManager.ServiceModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Blazor;
using ServiceStack.Blazor.Components;
using System.Text.Json.Serialization;

namespace MembershipManager.Client.Pages.Secure;

public partial class Units
{
    private bool isNotesOpen = false;
    private bool isSchoolsOpen = false;
    private bool isEventsOpen = false;

    private List<int> noteIds = [];

    void ConfigureNotesQuery(QueryBase query)
    {
        query.AddQueryParam(nameof(QueryNotes.Ids), JsonConvert.SerializeObject(noteIds));
    }

    protected void OnNotesClicked(Unit unit)
    {
        isNotesOpen = true;
        noteIds = unit.NotesLink.Select(x => x.NoteId).ToList();
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
