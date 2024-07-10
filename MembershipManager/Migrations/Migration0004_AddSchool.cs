using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0004_AddSchool : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<School>();
    }

    public override void Down()
    {
        Db.DropTable<School>();
    }
}
