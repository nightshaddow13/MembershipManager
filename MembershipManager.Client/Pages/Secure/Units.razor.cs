using MembershipManager.ServiceModel;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Blazor.Components.Tailwind;

namespace MembershipManager.Client.Pages.Secure;

public partial class Units
{
    private bool IsNotesOpen { get; set; } = false;
    private bool IsSchoolsOpen { get; set; } = false;
    private bool IsEventsOpen { get; set; } = false;

    private List<int> _noteIds = [];
    private List<int> _eventIds = [];
    private List<int> _schoolIds = [];
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
        IsNotesOpen = true;
        StateHasChanged();
    }

    protected void CloseNotes()
    {
        IsNotesOpen = false;
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
        IsEventsOpen = true;
        StateHasChanged();
    }

    protected void CloseEvents()
    {
        IsEventsOpen = false;
    }

    #endregion Events

    protected void OnSchoolsClicked(Unit unit)
    {
        _unit = unit;
        _schoolIds = unit.SchoolsLink.Select(x => x.SchoolId).ToList();
        IsSchoolsOpen = true;
        StateHasChanged();
    }

    protected async void CloseSchools()
    {
        IsSchoolsOpen = false;
        await ReloadGrid();
    }

    void ConfigureSchoolsQuery(QueryBase query)
    {
        query.AddQueryParam(nameof(QueryUnitSchool.Ids), JsonConvert.SerializeObject(_schoolIds));
    }
}
