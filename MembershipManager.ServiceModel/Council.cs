using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MembershipManager.ServiceModel;

#region Base definition

public class Council : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    [Reference]
    public List<District> Districts { get; set; } = [];
}

#endregion

#region Interactions

[AutoApply(Behavior.AuditQuery)]
public class QueryCouncil : QueryDb<Council> { }

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditCreate)]
public class CreateCouncil : ICreateDb<Council>, IReturn<IdResponse>
{
    [ValidateMaximumLength(25)]
    public string Description { get; set; } = string.Empty;
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditModify)]
public class UpdateCouncil : IPatchDb<Council>, IReturn<IdResponse>
{
    public int Id { get; set; }

    [ValidateMaximumLength(25)]
    public string Description { get; set; } = string.Empty;
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteCouncil : IDeleteDb<Council>, IReturnVoid
{
    public int Id { get; set; }
}

#endregion
