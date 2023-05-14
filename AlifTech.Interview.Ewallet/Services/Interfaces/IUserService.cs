namespace AlifTech.Interview.Ewallet.Services.Interfaces;

public interface IUserService
{
    Task<string> GetUserSecretKeyAsync(string userId);
}