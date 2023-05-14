using FluentMigrator;

namespace AlifTech.Interview.Ewallet.Data.Migrations;

[Migration(0001, "Create Users Table")]
public class M0001_Create_Users_Table : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("M0001_Create_Users_Table.sql");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}