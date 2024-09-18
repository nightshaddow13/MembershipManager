using MembershipManager.ServiceModel;
using Microsoft.AspNetCore.Components;
using ServiceStack;

namespace MembershipManager.Client.Pages.SharedComponents
{
    public partial class SchoolMultiSelectBase : ComponentBase
    {
        [Inject]protected JsonApiClient? ServiceClient { get; set; }
        [Parameter] public List<School> SelectedSchools { get; set; } = new();
        [Parameter] public List<UnitSchool> SelectedPairs { get; set; } = new();
        [Parameter] public EventCallback<List<School>> SelectedSchoolsChanged { get; set; }
        [Parameter] public EventCallback<List<UnitSchool>> SelectedPairsChanged { get; set; }

        protected List<School> AllSchools { get; set; } = new();
        protected string SearchTerm { get; set; } = "";
        protected bool IsDropdownVisible { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (ServiceClient == null)
            {
                throw new InvalidOperationException("ServiceClient is not initialized");
            }

            var response = await ServiceClient.GetAsync(new QuerySchool());
            AllSchools = response.Results;
        }

        protected List<School> FilteredSchools => AllSchools
            .Where(s => !SelectedSchools.Contains(s) && s.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        protected async Task SelectSchool(School school)
        {
            SelectedSchools.Add(school);
            SearchTerm = "";
            await SelectedSchoolsChanged.InvokeAsync(SelectedSchools);
        }

        protected async Task RemoveSchool(School school)
        {
            SelectedSchools.Remove(school);
            UnitSchool pair = SelectedPairs
                .Where(s => s.School == school).First();
            SelectedPairs.Remove(pair);
            await SelectedSchoolsChanged.InvokeAsync(SelectedSchools);
        }

        protected void ShowDropdown() => IsDropdownVisible = true;

        protected async Task HideDropdownDelayed()
        {
            await Task.Delay(200);
            IsDropdownVisible = false;
            StateHasChanged();
        }
    }

}
