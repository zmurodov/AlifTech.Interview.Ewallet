using System.Data;
using AlifTech.Interview.Ewallet.Entities;

namespace AlifTech.Interview.Ewallet.Repositories.Interfaces;

public interface IWalletTypeRepository
{
    Task<WalletType> GetWalletTypeAsync(int walletTypeId, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default);
}