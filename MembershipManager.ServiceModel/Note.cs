using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MembershipManager.ServiceModel;

#region Base definition

public class Note : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    [Reference]
    public List<SchoolNote> SchoolsLink { get; set; } = [];

    [Reference]
    public List<UnitNote> UnitsLink { get; set; } = [];
    
    [Reference]
    public List<EventNote> EventsLinks { get; set; } = [];
}

#endregion

#region Interactions

[AutoApply(Behavior.AuditQuery)]
public class QueryNote : QueryDb<Note> { }

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditCreate)]
public class CreateNote : ICreateDb<Note>, IReturn<IdResponse>
{
    public string Description { get; set; } = string.Empty;
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditModify)]
public class UpdateNote : IPatchDb<Note>, IReturn<IdResponse>
{
    public int Id { get; set; }

    [ValidateMaximumLength(25)]
    public string Description { get; set; } = string.Empty;
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteNote : IDeleteDb<Note>, IReturnVoid
{
    public int Id { get; set; }
}

#endregion
