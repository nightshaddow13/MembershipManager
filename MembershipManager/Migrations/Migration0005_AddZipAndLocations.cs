using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0005_AddZipAndLocations : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<ZipCode>();
        Db.CreateTable<Location>();
    }

    public override void Down()
    {
        Db.DropTable<Location>();
        Db.DropTable<ZipCode>();
    }
}
