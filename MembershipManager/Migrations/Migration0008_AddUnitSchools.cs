using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0008_AddUnitSchools : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<UnitSchool>();
    }

    public override void Down()
    {
        Db.DropTable<UnitSchool>();
    }
}
