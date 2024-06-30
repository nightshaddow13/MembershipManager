using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0003_AddDistrictToUnit : MigrationBase
{
    public override void Up()
    {
        Db.DropAndCreateTable<Unit>();
    }

    public override void Down()
    {
        Db.DropAndCreateTable<Unit>();
    }
}
