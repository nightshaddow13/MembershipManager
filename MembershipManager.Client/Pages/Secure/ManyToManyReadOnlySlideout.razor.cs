using Microsoft.AspNetCore.Components;
using ServiceStack;

namespace MembershipManager.Client.Pages.Secure;

public partial class ManyToManyReadOnlySlideout<
    TModel,
    TDeleteModel,
    TQueryModel>
{
    [Inject] public JsonApiClient? Client { get; set; }
    [Inject] public NavigationManager? NavigationManager { get; set; }

    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter] public EventCallback OnSubmitSuccess { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public RenderFragment Columns { get; set; } = default!;
    [Parameter, EditorRequired] public Action<QueryBase> ConfigureQueryCallback { get; set; } = default!;

    private void ConfigureQuery(QueryBase query)
    {
        if (query != null)
            ConfigureQueryCallback.Invoke(query);
    }
}
