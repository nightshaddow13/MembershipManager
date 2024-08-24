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

    private List<int> _noteIds = [];
    private List<int> _eventIds = [];
    private Unit _unit = new();

    private AutoQueryGrid<Unit> _unitGrid = default!;

    #region Notes

    void ConfigureNotesQuery(QueryBase query)
    {
        query.AddQueryParam(nameof(QueryNotes.Ids), JsonConvert.SerializeObject(_noteIds));
    }

    protected void OnNotesClicked(Unit unit)
    {
        _unit = unit;
        _noteIds = unit.NotesLink.Select(x => x.NoteId).ToList();
        isNotesOpen = true;
        StateHasChanged();
    }

    protected CreateUnitNote CreateUnitNote(int unitId, int noteId) => new()
    {
        UnitId = unitId,
        NoteId = noteId
    };

    #endregion Notes

    protected async Task ReloadGrid()
    {
        await _unitGrid.RefreshAsync();
    }

    #region Events

    void ConfigureEventsQuery(QueryBase query)
    {
        query.AddQueryParam(nameof(QueryEventUnits.Ids), JsonConvert.SerializeObject(_eventIds));
    }

    protected void OnEventsClicked(Unit unit)
    {
        _unit = unit;
        _eventIds = unit.EventsLink.Select(x => x.EventId).ToList();
        isEventsOpen = true;
        StateHasChanged();
    }

    #endregion Events

    protected void OnSchoolsClicked(MouseEventArgs args)
    {
        isSchoolsOpen = true;
        StateHasChanged();
    }
}
