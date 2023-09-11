using Models;
using ViewModels;

namespace Services
{
    public interface IUserService
    {
        Task<bool> IsExistEmailAsync(string email);
        Task AddUserAsync(RegisterUserViewModel registerUserViewModel);
        Task<UpdateUserViewModel> GetUserAsync(int userId);
        Task UpdateUserAsync(int userId, UpdateUserViewModel updateUserViewModel);
    }
}
