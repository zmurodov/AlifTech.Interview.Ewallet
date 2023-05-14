using System.Data;
using AlifTech.Interview.Ewallet.Entities;

namespace AlifTech.Interview.Ewallet.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserAsync(string userId, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default);
}