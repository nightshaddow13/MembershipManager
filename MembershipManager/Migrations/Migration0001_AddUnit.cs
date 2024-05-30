using MembershipManager.ServiceModel;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace MembershipManager.Migrations;

public class Migration0001_AddUnit : MigrationBase
{
    public class Unit : AuditBase
    {
        [AutoIncrement]
        public int Id { get; set; }
        // todo: add district
        public UnitType Type { get; set; }
        public Sex Sex { get; set; }
        public int Number { get; set; }
    }

    public override void Up()
    {
        Db.CreateTable<Unit>();
    }

    public override void Down()
    {
        Db.DropTable<Unit>();
    }
}
