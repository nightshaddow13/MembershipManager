using Microsoft.AspNetCore.Components;
using ServiceStack;
using ServiceStack.Blazor;

namespace MembershipManager.Client.Pages.Secure;

public partial class ManyToManyCreateSlideout<
    TModel, 
    TRelationshipModel, 
    TCreateModel, 
    TCreateRelationshipModel, 
    TUpdateModel, 
    TDeleteModel, 
    TQueryModel>
{
    [Inject] public JsonApiClient? Client { get; set; }
    [Inject] public NavigationManager? NavigationManager { get; set; }

    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter] public EventCallback OnSubmitSuccess { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public RenderFragment Columns { get; set; } = default!;
    [Parameter] public int OriginalLinkingId { get; set; }
    [Parameter, EditorRequired] public Action<QueryBase> ConfigureQueryCallback { get; set; } = default!;
    [Parameter, EditorRequired] public CreateRelationshipDelegate CreateRelationshipModelRequestCallback { get; set; } = default!;

    ApiResult<IdResponse>? EditModelApi { get; set; }
    ApiResult<IdResponse>? EditReplationshipModelApi { get; set; }
    Dictionary<string, object> ModelDictionary { get; set; } = [];
    Dictionary<string, object> RelationshipModelDictionary { get; set; } = [];

    private void ConfigureQuery(QueryBase query)
    {
        if (query != null)
            ConfigureQueryCallback.Invoke(query);
    }

    protected override Task OnInitializedAsync()
    {
        ModelDictionary = new TModel().ToModelDictionary();
        RelationshipModelDictionary = new TRelationshipModel().ToModelDictionary();

        return base.OnInitializedAsync();
    }

    // todo: need to add some notifications
    private async Task submit()
    {
        var noteRequest = ModelDictionary.FromModelDictionary<TCreateModel>();
        EditModelApi = await Client!.ApiAsync(noteRequest);

        // todo: add some better error handling
        if (EditModelApi.Failed || EditModelApi.Response == null)
            return;

        var eventNoteRequest = CreateRelationshipModelRequestCallback(OriginalLinkingId, int.Parse(EditModelApi.Response!.Id));
        EditReplationshipModelApi = await Client!.ApiAsync(eventNoteRequest);

        if (EditReplationshipModelApi.Failed)
            return;

        await IsOpenChanged.InvokeAsync(false);
        NavigationManager?.NavigateTo(NavigationManager.Uri.Split("?")[0]);

        await OnSubmitSuccess.InvokeAsync();
    }

    public delegate TCreateRelationshipModel CreateRelationshipDelegate(int originalLinkingId, int newModelId);
}
