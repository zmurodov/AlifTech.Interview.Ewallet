using FluentMigrator;

namespace AlifTech.Interview.Ewallet.Data.Migrations;

[Migration(0002, "Create WalletType Table")]
public class M0002_Create_WalletType_Table : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("M0002_Create_WalletType_Table.sql");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}