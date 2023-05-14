using System.Data;
using AlifTech.Interview.Ewallet.Entities;

namespace AlifTech.Interview.Ewallet.Repositories.Interfaces;

public interface IWalletRepository
{
    Task<bool> IsWalletExistsAsync(string walletId, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default);

    Task<Wallet> GetWalletAsync(string walletId, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateBalanceAsync(string walletId, decimal balance, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default);

    Task InsertReplenishmentAsync(string userId, string walletId, decimal amount,
        IDbTransaction transaction = null,
        CancellationToken cancellationToken = default);

    Task<List<Replenishment>> GetReplenishmentsForMonthAsync(string walletId, DateTime month,
        IDbTransaction transaction = null, CancellationToken cancellationToken = default);

    Task<int> GetReplenishmentsCountForMonthAsync(string walletId, DateTime month,
        IDbTransaction transaction = null, CancellationToken cancellationToken = default);

    Task<decimal> GetReplenishmentsSumForMonthAsync(string walletId, DateTime month,
        IDbTransaction transaction = null, CancellationToken cancellationToken = default);
}