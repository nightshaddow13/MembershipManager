using MembershipManager.ServiceModel;
using Microsoft.AspNetCore.Components;
using ServiceStack;

namespace MembershipManager.Client.Pages.SharedComponents
{
    public partial class UnitSchoolSlideoutBase : ComponentBase
    {
        [Inject] public JsonApiClient? Client { get; set; }
        [Parameter] public int UnitId { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public bool IsOpen { get; set; }
        [Parameter] public string Title { get; set; } = string.Empty;
        protected List<School> SelectedSchools { get; set; } = new();
        protected List<UnitSchool> SelectedPairs { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var response = await Client!.GetAsync(new QueryUnitSchools { UnitId = UnitId });
            SelectedPairs = response.Results.ToList();
            SelectedSchools = response.Results.Select(us => us.School).ToList();
        }

        protected async Task SaveChanges()
        {
            var existingSchools = await Client!.GetAsync(new QueryUnitSchools { UnitId = UnitId });
            var existingSchoolIds = existingSchools.Results.Select(us => us.SchoolId).ToList();
            var selectedSchoolIds = SelectedSchools.Select(s => s.Id).ToList();

            var schoolsToAdd = selectedSchoolIds.Except(existingSchoolIds);
            var schoolsToRemove = existingSchoolIds.Except(selectedSchoolIds);
            var existingPairsToRemove = existingSchools.Results
                .Where(u => u.UnitId == UnitId && schoolsToRemove.Contains(u.SchoolId) ).ToList();

            foreach (var schoolId in schoolsToAdd)
            {
                await Client!.PostAsync(new CreateUnitSchool { UnitId = UnitId, SchoolId = schoolId });
            }

            foreach (var schoolId in existingPairsToRemove)
            {
                await Client!.DeleteAsync(new DeleteUnitSchool { Id = schoolId.Id });
            }

            await OnClose.InvokeAsync();
        }

        protected async Task Close()
        {
            IsOpen = false;
            await OnClose.InvokeAsync();
        } 

    }
}
