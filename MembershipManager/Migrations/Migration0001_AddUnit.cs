using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0001_AddUnit : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<Unit>();
    }

    public override void Down()
    {
        Db.DropTable<Unit>();
    }
}
