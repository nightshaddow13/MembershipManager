using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0002_AddCouncilAndDistrict : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<Council>();
        Db.CreateTable<District>();
    }

    public override void Down()
    {
        Db.DropTable<District>();
        Db.DropTable<Council>();
    }
}
