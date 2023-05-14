using System.Data;
using AlifTech.Interview.Ewallet.Data;
using AlifTech.Interview.Ewallet.Entities;
using AlifTech.Interview.Ewallet.Repositories.Interfaces;
using Dapper;

namespace AlifTech.Interview.Ewallet.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDapperDbContext _dbContext;

    public UserRepository(IDapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserAsync(string userId, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        var sql = @"SELECT * FROM ""users"" WHERE ""id"" = @UserId";
        
        var user = await _dbContext.Connection
            .QueryFirstOrDefaultAsync<User>(sql, new {UserId = userId}, transaction: transaction)
            .ConfigureAwait(false);
        
        return user;
    }
}