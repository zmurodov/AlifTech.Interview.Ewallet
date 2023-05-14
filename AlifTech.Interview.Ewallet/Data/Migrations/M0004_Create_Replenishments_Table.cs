using FluentMigrator;

namespace AlifTech.Interview.Ewallet.Data.Migrations;

[Migration(0004, "Create Replenishments Table")]
public class M0003_Create_Replenishments_Table : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("M0004_Create_Replenishments_Table.sql");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}