using System.Data;
using AlifTech.Interview.Ewallet.Data;
using AlifTech.Interview.Ewallet.Entities;
using AlifTech.Interview.Ewallet.Repositories.Interfaces;
using Dapper;

namespace AlifTech.Interview.Ewallet.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly IDapperDbContext _context;

    public WalletRepository(IDapperDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsWalletExistsAsync(string walletId, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        var sql = @"SELECT COUNT(*) FROM wallets WHERE id = @WalletId";

        var count = await _context.Connection
            .QueryFirstOrDefaultAsync<int>(sql, new { WalletId = walletId }, transaction: transaction)
            .ConfigureAwait(false);

        return count > 0;
    }

    public async Task<Wallet> GetWalletAsync(string walletId, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        var sql =
            @"SELECT id as Id, user_id as UserId, balance as Balance, wallet_type_id as WalletTypeId FROM wallets WHERE id = @WalletId";

        var wallet = await _context.Connection
            .QueryFirstOrDefaultAsync<Wallet>(sql, new { WalletId = walletId }, transaction: transaction)
            .ConfigureAwait(false);

        return wallet;
    }

    public async Task<bool> UpdateBalanceAsync(string walletId, decimal newBalance, decimal oldBalance,
        IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        var sql = @"UPDATE wallets SET balance = @NewBalance WHERE id = @WalletId AND balance = @OldBalance";

        var affectedRows = await _context.Connection
            .ExecuteAsync(sql, new { WalletId = walletId, NewBalance = newBalance, OldBalance = oldBalance },
                transaction: transaction)
            .ConfigureAwait(false);

        return affectedRows > 0;
    }

    public async Task InsertReplenishmentAsync(string userId, string walletId, decimal amount,
        IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        var sql =
            @"INSERT INTO replenishments (user_id, amount, wallet_id, date) VALUES (@UserId, @Amount, @WalletId, NOW())";

        await _context.Connection
            .ExecuteAsync(sql, new { UserId = userId, Amount = amount, WalletId = walletId }, transaction: transaction)
            .ConfigureAwait(false);
    }

    public async Task<List<Replenishment>> GetReplenishmentsForMonthAsync(string walletId, DateTime month,
        IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        var sql = "SELECT id as Id, user_id as UserId, amount as Amount, wallet_id as WalletId, date as Date FROM replenishments WHERE wallet_id = @WalletId AND date_trunc('month', date) = @Month";

        var replenishments = await _context.Connection
            .QueryAsync<Replenishment>(sql, new { WalletId = walletId, Month = month }, transaction: transaction)
            .ConfigureAwait(false);

        return replenishments.ToList();
    }

    public async Task<int> GetReplenishmentsCountForMonthAsync(string walletId, DateTime month,
        IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        var sql =
            "SELECT COUNT(*) FROM replenishments WHERE wallet_id = @WalletId AND date_trunc('month', date) = @Month";

        var count = await _context.Connection
            .QueryFirstOrDefaultAsync<int>(sql, new { WalletId = walletId, Month = month }, transaction: transaction)
            .ConfigureAwait(false);

        return count;
    }

    public async Task<decimal> GetReplenishmentsSumForMonthAsync(string walletId, DateTime month,
        IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        var sql =
            "SELECT SUM(amount) FROM replenishments WHERE wallet_id = @WalletId AND date_trunc('month', date) = @Month";

        var sum = await _context.Connection
            .QueryFirstOrDefaultAsync<decimal?>(sql, new { WalletId = walletId, Month = month }, transaction: transaction)
            .ConfigureAwait(false);

        return sum ?? 0;
    }
}