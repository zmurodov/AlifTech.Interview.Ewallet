using System.Data;
using AlifTech.Interview.Ewallet.Data;
using AlifTech.Interview.Ewallet.Exceptions;
using AlifTech.Interview.Ewallet.Models;
using AlifTech.Interview.Ewallet.Repositories.Interfaces;
using AlifTech.Interview.Ewallet.Services.Interfaces;
using Polly;

namespace AlifTech.Interview.Ewallet.Services;

public class WalletService : IWalletService
{
    private readonly IDapperDbContext _context;
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletTypeRepository _walletTypeRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WalletService(
        IDapperDbContext context,
        IWalletRepository walletRepository,
        IWalletTypeRepository walletTypeRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _walletRepository = walletRepository;
        _walletTypeRepository = walletTypeRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> IsWalletExistsAsync(string walletId, CancellationToken cancellationToken = default)
    {
        var exists = await _walletRepository.IsWalletExistsAsync(walletId, cancellationToken: cancellationToken);
        return exists;
    }

    public async Task<decimal> GetBalanceAsync(string walletId, CancellationToken cancellationToken = default)
    {
        var wallet = await _walletRepository.GetWalletAsync(walletId, cancellationToken: cancellationToken);

        if (wallet == null)
        {
            throw new WalletNotFoundException("Wallet not found");
        }

        return wallet.Balance;
    }

    public async Task<bool> UpdateBalanceAsync(string walletId, decimal amount,
        CancellationToken cancellationToken = default)
    {
        var retryPolicy = Policy
            .Handle<DBConcurrencyException>()
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(1 * retryAttempt));

        var updated = await retryPolicy.ExecuteAsync(async () =>
            await UpdateBalanceInternalAsync(walletId, amount, cancellationToken)
                .ConfigureAwait(false));

        return updated;
    }

    private async Task<bool> UpdateBalanceInternalAsync(string walletId, decimal amount,
        CancellationToken cancellationToken = default)
    {
        using var transaction = _context.BeginTransaction();

        var wallet = await _walletRepository
            .GetWalletAsync(walletId, transaction: transaction, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (wallet == null)
        {
            throw new WalletNotFoundException("Wallet not found");
        }

        var walletType = await _walletTypeRepository.GetWalletTypeAsync(
                wallet.WalletTypeId,
                transaction: transaction,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (walletType == null)
        {
            throw new WalletTypeNotFoundException("Wallet type not found");
        }

        var oldBalance = wallet.Balance;
        var newBalance = wallet.Balance + amount;

        if (newBalance > walletType.MaxBalance)
        {
            throw new WalletBalanceException(
                $"Wallet balance cannot be more than maximum balance ({walletType.MaxBalance}) of wallet type {walletType.Name}");
        }

        var updated = await _walletRepository.UpdateBalanceAsync(
                walletId,
                newBalance: newBalance,
                oldBalance: oldBalance,
                transaction: transaction,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!updated)
        {
            throw new DBConcurrencyException("Wallet balance was updated by another transaction");
        }

        transaction.Commit();
        
        return true;
    }
}