using FluentMigrator.Runner;

namespace AlifTech.Interview.Ewallet.Data;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host, string databaseName)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var databaseService = services.GetRequiredService<Database>();
        var migrationService = services.GetRequiredService<IMigrationRunner>();
        
        try
        {
            // create database if not exists
            databaseService.CreateDatabase(databaseName);
            
            // migrate application database. this is database first approach
            migrationService.ListMigrations();
            migrationService.MigrateUp();
        }
        catch
        {
            throw;
        }

        return host;
    }
}