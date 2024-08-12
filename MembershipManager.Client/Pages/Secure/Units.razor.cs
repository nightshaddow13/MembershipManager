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
    [Inject] public JsonApiClient? Client { get; set; }

    private bool isNotesOpen = false;
    private bool isSchoolsOpen = false;
    private bool isEventsOpen = false;

    private List<int> noteIds = [];

    IHasErrorStatus? EditNoteApi { get; set; }
    IHasErrorStatus? EditUnitNoteApi { get; set; }
    Dictionary<string, object> CreateModelDictionary { get; set; } = [];
    Dictionary<string, object> CreateNoteModelDictionary { get; set; } = [];

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

    void Configure(QueryBase query)
    {
        query.AddQueryParam(nameof(QueryNotes.Ids), JsonConvert.SerializeObject(noteIds));
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        EditNoteApi = null;

        CreateModelDictionary = new Note().ToModelDictionary();
        CreateNoteModelDictionary = new EventNote().ToModelDictionary();
    }

    async Task submit()
    {
        var noteRequest = CreateModelDictionary.FromModelDictionary<CreateEvent>();
        EditNoteApi = await Client!.ApiAsync(noteRequest);

        var eventNoteRequest = CreateNoteModelDictionary.FromModelDictionary<CreateEventNote>();
        EditUnitNoteApi = await Client!.ApiAsync(eventNoteRequest);
    }
}
