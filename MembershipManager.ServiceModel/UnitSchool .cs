using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MembershipManager.ServiceModel;

#region Base definition

[UniqueConstraint(nameof(UnitId), nameof(SchoolId))]
public class UnitSchool : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

    [Ref(Model = nameof(Unit), RefId = nameof(Unit.Id), RefLabel = nameof(Unit.Number))]
    [ForeignKey(typeof(Unit))]
    public int UnitId { get; set; }

    [ForeignKey(typeof(School))]
    public int SchoolId { get; set; }
}

#endregion

#region Interactions

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditQuery)]
public class QueryUnitSchool : QueryDb<UnitSchool> { }

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditCreate)]
public class CreateUnitSchool : ICreateDb<UnitSchool>, IReturn<IdResponse>
{
    public int UnitId { get; set; }
    public int SchoolId { get; set; }
}

[ValidateHasRole(Roles.Committee)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditModify)]
public class UpdateUnitSchool : IPatchDb<UnitSchool>, IReturn<IdResponse>
{
    public int UnitId { get; set; }
    public int SchoolId { get; set; }
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteUnitSchool : IDeleteDb<UnitSchool>, IReturnVoid
{
    public int Id { get; set; }
}

#endregion