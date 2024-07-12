using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0006_AddLocationToSchools : MigrationBase
{
    public override void Up()
    {
        Db.DropAndCreateTable<School>();
    }

    public override void Down()
    {
        Db.DropAndCreateTable<School>();
    }
}
