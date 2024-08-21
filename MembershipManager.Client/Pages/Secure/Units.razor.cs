using MembershipManager.ServiceModel;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Blazor.Components.Tailwind;

namespace MembershipManager.Client.Pages.Secure;

public partial class Units
{
    private bool isNotesOpen = false;
    private bool isSchoolsOpen = false;
    private bool isEventsOpen = false;

    private List<int> noteIds = [];
    private Unit _unit = new();

    private AutoQueryGrid<Unit> _unitGrid = default!;

    void ConfigureNotesQuery(QueryBase query)
    {
        query.AddQueryParam(nameof(QueryNotes.Ids), JsonConvert.SerializeObject(noteIds));
    }

    protected void OnNotesClicked(Unit unit)
    {
        _unit = unit;
        noteIds = unit.NotesLink.Select(x => x.NoteId).ToList();
        isNotesOpen = true;
        StateHasChanged();
    }

    protected CreateUnitNote CreateUnitNote(int unitId, int noteId) => new()
    {
        UnitId = unitId,
        NoteId = noteId
    };

    protected async Task ReloadGrid()
    {
        await _unitGrid.RefreshAsync();
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
