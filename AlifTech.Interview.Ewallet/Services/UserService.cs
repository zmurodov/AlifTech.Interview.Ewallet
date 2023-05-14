using AlifTech.Interview.Ewallet.Repositories.Interfaces;
using AlifTech.Interview.Ewallet.Services.Interfaces;

namespace AlifTech.Interview.Ewallet.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> GetUserSecretKeyAsync(string userId)
    {
        var user = await _userRepository.GetUserAsync(userId);
        
        return user?.Secret;
    }
}