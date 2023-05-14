using AlifTech.Interview.Ewallet.Entities;
using AlifTech.Interview.Ewallet.Models;

namespace AlifTech.Interview.Ewallet.Services.Interfaces;

public interface IWalletService
{
    Task<bool> IsWalletExistsAsync(string walletId, CancellationToken cancellationToken = default);
    Task<decimal> GetBalanceAsync(string walletId, CancellationToken cancellationToken = default);
    Task<bool> UpdateBalanceAsync(string walletId, decimal amount, CancellationToken cancellationToken = default);
}