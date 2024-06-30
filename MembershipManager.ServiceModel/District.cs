using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MembershipManager.ServiceModel;

#region Base definition

public class District : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    [ForeignKey(typeof(Council))]
    public int CouncilId { get; set; }

    [Reference]
    public List<Unit> Units { get; set; } = [];
}

#endregion

#region Interactions

[AutoApply(Behavior.AuditQuery)]
public class QueryDistrict : QueryDb<District> { }

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditCreate)]
public class CreateDistrict : ICreateDb<District>, IReturn<IdResponse>
{
    [ValidateMaximumLength(25)]
    public string Description { get; set; } = string.Empty;

    public int CouncilId { get; set; }
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditModify)]
public class UpdateDistrict : IPatchDb<District>, IReturn<IdResponse>
{
    public int Id { get; set; }

    [ValidateMaximumLength(25)]
    public string Description { get; set; } = string.Empty;

    public int CouncilId { get; set; }
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteDistrict : IDeleteDb<District>, IReturnVoid
{
    public int Id { get; set; }
}

#endregion
