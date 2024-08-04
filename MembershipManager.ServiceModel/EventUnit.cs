using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MembershipManager.ServiceModel;

#region Base definition

[UniqueConstraint(nameof(EventId), nameof(UnitId))]
public class EventUnit : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

    [Ref(Model = nameof(Event), RefId = nameof(Event.Id), RefLabel = nameof(Event.Description))]
    [References(typeof(Event))]
    public int EventId { get; set; }

    [Ref(Model = nameof(Unit), RefId = nameof(Unit.Id), RefLabel = nameof(Unit.Number))]
    [References(typeof(Unit))]
    public int UnitId { get; set; }
}

#endregion

#region Interactions

[Tag("Units"), Description("Find Event & Unit links")]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditQuery)]
public class QueryEventUnit : QueryDb<EventUnit> { }

[Tag("Units"), Description("Link an Event to a Unit")]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditCreate)]
public class CreateEventUnit : ICreateDb<EventUnit>, IReturn<IdResponse>
{
    public int EventId { get; set; }
    public int UnitId { get; set; }
}

[Tag("Units"), Description("Delete a link of an Event to a Unit")]
[ValidateHasRole(Roles.MembershipChair)]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteEventUnit : IDeleteDb<EventUnit>, IReturnVoid
{
    public int Id { get; set; }
}

#endregion