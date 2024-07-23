using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MembershipManager.ServiceModel;

#region Base definition

[UniqueConstraint(nameof(EventId), nameof(UnitId))]
public class EventUnit : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

    [ForeignKey(typeof(Event))]
    public int EventId { get; set; }

    [ForeignKey(typeof(Unit))]
    public int UnitId { get; set; }
}

#endregion

#region Interactions

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditQuery)]
public class QueryEventUnit : QueryDb<EventUnit> { }

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditCreate)]
public class CreateEventUnit : ICreateDb<EventUnit>, IReturn<IdResponse>
{
    public int EventId { get; set; }
    public int UnitId { get; set; }
}

[ValidateHasRole(Roles.Committee)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditModify)]
public class UpdateEventUnit : IPatchDb<EventUnit>, IReturn<IdResponse>
{
    public int EventId { get; set; }
    public int UnitId { get; set; }
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteEventUnit : IDeleteDb<EventUnit>, IReturnVoid
{
    public int Id { get; set; }
}

#endregion