using System.Data;
using AlifTech.Interview.Ewallet.Data;
using AlifTech.Interview.Ewallet.Entities;
using AlifTech.Interview.Ewallet.Repositories.Interfaces;
using Dapper;

namespace AlifTech.Interview.Ewallet.Repositories;

public class WalletTypeRepository : IWalletTypeRepository
{
    private readonly IDapperDbContext _context;

    public WalletTypeRepository(IDapperDbContext context)
    {
        _context = context;
    }

    public async Task<WalletType> GetWalletTypeAsync(int walletTypeId, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        var sql = @"SELECT id as Id, name as Name, max_balance as MaxBalance FROM wallet_types WHERE id = @WalletTypeId";
        var walletType = await _context.Connection
            .QueryFirstOrDefaultAsync<WalletType>(sql, new { WalletTypeId = walletTypeId }, transaction: transaction)
            .ConfigureAwait(false);

        return walletType;
    }
}