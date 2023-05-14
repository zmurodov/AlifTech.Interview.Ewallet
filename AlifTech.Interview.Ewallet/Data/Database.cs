using System.Data;
using Dapper;
using Npgsql;

namespace AlifTech.Interview.Ewallet.Data;

public class Database
{
    private readonly string _connectionString;
    
    public Database(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Master");
    }
    
    public void CreateDatabase(string databaseName)
    {
        var query = "select datname from pg_database where datname = @DbName;";
        var parameters = new DynamicParameters();
        parameters.Add("@DbName", databaseName, DbType.String);
        
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        var result = connection.Query(query, parameters);
        if(!result.Any())
            connection.Execute($"CREATE DATABASE \"{databaseName}\";");
    }
}