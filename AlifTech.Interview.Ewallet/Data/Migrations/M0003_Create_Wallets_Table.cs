using FluentMigrator;

namespace AlifTech.Interview.Ewallet.Data.Migrations;

[Migration(0003, "Create Wallets Table")]
public class M0002_Create_Wallets_Table : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("M0003_Create_Wallets_Table.sql");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}