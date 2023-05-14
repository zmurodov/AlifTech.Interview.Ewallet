using FluentMigrator;

namespace AlifTech.Interview.Ewallet.Data.Migrations;

[Migration(0005, "Seed Data")]
public class M0004_Seed_Data : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("M0005_Seed_Data.sql");
    }

    public override void Down()
    {
        Execute.EmbeddedScript("M0005_Seed_Data_Down.sql");
    }
}