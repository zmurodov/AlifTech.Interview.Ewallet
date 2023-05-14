using System.Data;
using Npgsql;

namespace AlifTech.Interview.Ewallet.Data;

public class DapperDbContext : IDapperDbContext
{
    protected readonly IDbConnection InnerConnection;

    public DapperDbContext(IConfiguration configuration)
    {
        InnerConnection = new NpgsqlConnection(configuration.GetConnectionString("Default"));
    }

    public IDbConnection Connection
    {
        get
        {
            OpenConnection();
            return InnerConnection;
        }
    }

    public void OpenConnection()
    {
        if (InnerConnection.State != ConnectionState.Open && InnerConnection.State != ConnectionState.Connecting)
            InnerConnection.Open();
    }

    public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        return Connection.BeginTransaction(isolationLevel);
    }

    public void Dispose()
    {
        InnerConnection?.Dispose();
    }
}