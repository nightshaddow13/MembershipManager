using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MembershipManager.ServiceModel;

#region Base definition

public class Unit : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

    [ForeignKey(typeof(District))]
    public int DistrictId { get; set; }

    public UnitType Type { get; set; }
    public Sex Sex { get; set; }
    public int Number { get; set; }

    [Reference]
    public List<EventUnit> EventsLink { get; set; } = [];

    [Reference]
    public List<UnitSchool> SchoolsLink { get; set; } = [];

    [Reference]
    public List<UnitNote> NotesLink { get; set; } = [];
}

public enum UnitType
{
    Pack,
    Troop,
    Crew,
    Ship
}

public enum Sex
{
    Family,
    Male,
    Female
}

#endregion

#region Interactions

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditQuery)]
public class QueryUnit : QueryDb<Unit> { }

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditCreate)]
public class CreateUnit : ICreateDb<Unit>, IReturn<IdResponse>
{
    [ApiAllowableValues(typeof(Sex))]
    public Sex Sex { get; set; }

    [ApiAllowableValues(typeof(UnitType))]
    public UnitType Type { get; set; }

    [ValidateGreaterThan(0)]
    public int Number { get; set; }

    public int DistrictId { get; set; }
}

[ValidateHasRole("Admin")]
[ValidateHasRole("MembershipChair")]
[ValidateHasRole("CouncilExecutive")]
[ValidateHasRole("CommitteeMember")]
[ValidateHasRole("NewMemberCoordinator")]
[AutoApply(Behavior.AuditModify)]
public class UpdateUnit : IPatchDb<Unit>, IReturn<IdResponse>
{
    public int Id { get; set; }

    [ApiAllowableValues(typeof(Sex))]
    public Sex Sex { get; set; }

    [ApiAllowableValues(typeof(UnitType))]
    public UnitType Type { get; set; }

    [ValidateGreaterThan(0)]
    public int Number { get; set; }

    public int DistrictId { get; set; }
}

[ValidateHasRole("Admin")]
[ValidateHasRole("CommitteeMember")]
[ValidateHasRole("NewMemberCoordinator")]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteUnit : IDeleteDb<Unit>, IReturnVoid
{
    public int Id { get; set; }
}

#endregion