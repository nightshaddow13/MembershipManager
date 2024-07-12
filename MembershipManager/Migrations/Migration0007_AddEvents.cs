using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0007_AddEvents : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<Event>();
        Db.CreateTable<EventSchool>();
        Db.CreateTable<EventUnit>();
    }

    public override void Down()
    {
        Db.DropTable<EventUnit>();
        Db.DropTable<EventSchool>();
        Db.DropTable<Event>();
    }
}
