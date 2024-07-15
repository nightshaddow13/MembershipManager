using MembershipManager.ServiceModel;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0009_AddNotes : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<Note>();
        Db.CreateTable<UnitNote>();
        Db.CreateTable<SchoolNote>();
        Db.CreateTable<EventNote>();
    }

    public override void Down()
    {
        Db.DropTable<UnitNote>();
        Db.DropTable<SchoolNote>();
        Db.DropTable<EventNote>();
        Db.DropTable<Note>();
    }
}
