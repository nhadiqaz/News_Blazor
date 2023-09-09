using Models;
using ViewModels;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsExistEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task<bool> IsCorrectPasswordAsync(string newPassword, byte[] passwordHash, byte[] passwordSalt);
        Task<User> GetUserAsync(string phonenumberOremail);
    }
}
