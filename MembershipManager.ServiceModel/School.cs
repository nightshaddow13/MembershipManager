using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MembershipManager.ServiceModel;

#region Base definition

public class School : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    // todo: convert address to new table using zip code as a reference
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;

    public SchoolType SchoolType { get; set; }
    public GradeLevels GradeLevels { get; set; }

    // todo: add contact linkage
}

public enum GradeLevels
{
    gKG_5 = 1,
    gKG_8,
    gKG_12,
    gPK_5,
    gPK_8,
    gPK_12,
    g6_8,
    g6_12,
    g9_12
}

public enum SchoolType
{
    Public,
    Private
}

#endregion

#region Interactions

[AutoApply(Behavior.AuditQuery)]
public class QuerySchool : QueryDb<School> { }

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditCreate)]
public class CreateSchool : ICreateDb<School>, IReturn<IdResponse>
{
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;

    [ApiAllowableValues(typeof(SchoolType))]
    public SchoolType SchoolType { get; set; }

    [ApiAllowableValues(typeof(GradeLevels))]
    public GradeLevels GradeLevels { get; set; }
}

[ValidateHasRole(Roles.Committee)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[ValidateHasRole(Roles.Committee)]
[AutoApply(Behavior.AuditModify)]
public class UpdateSchool : IPatchDb<School>, IReturn<IdResponse>
{
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;

    [ApiAllowableValues(typeof(GradeLevels))]
    public GradeLevels GradeLevels { get; set; }
}

[ValidateHasRole(Roles.Admin)]
[ValidateHasRole(Roles.MembershipChair)]
[ValidateHasRole(Roles.CouncilExecutive)]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteSchool : IDeleteDb<School>, IReturnVoid
{
    public int Id { get; set; }
}

#endregion