using System.Data;

namespace AlifTech.Interview.Ewallet.Data;

public interface IDapperDbContext : IDisposable
{
    IDbConnection Connection { get; }
    
    void OpenConnection();
    
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}